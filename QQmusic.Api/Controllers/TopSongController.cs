using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QQmusic.Core.Entities;
using QQmusic.Core.Interfaces;
using QQmusic.Infrastructure.Repositories;
using QQmusic.Infrastructure.Resources;
using QQmusic.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using QQmusic.Api.Messages;
using QQmusic.Core.Entities.EntityModels;
using QQmusic.Infrastructure.Extensions;

namespace QQmusic.Api.Controllers
{
    [Route("api/songs")]
    public class TopSongController : BasicController
    {
        private readonly SongRepository _repository;
        private readonly IMapper _mapper;
        public TopSongController(
            SongRepository repository,
            IMapper mapper, IUrlHelper urlHelper, IPropertyMappingContainer propertyMappingContainer, ITypeHelperService typeHelperService) : base(urlHelper, propertyMappingContainer, typeHelperService)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetSongs")]
        public async Task<IActionResult> Get(SongParameters songParameters)
        {
            ValidateMapping<SongResource, Song>(songParameters.OrderBy);
            ValidateFields<SongResource>(songParameters.Fields);
            if (Results.Count != 0) return Results.First();

            var list = await _repository.GetAllAsync(songParameters);

            var resources = _mapper.Map<IEnumerable<Song>, IEnumerable<SongResource>>(list);

            var shapedResources = resources.ToDynamicIEnumerable(songParameters.Fields);

            CreateHeader(songParameters, list, "GetSongs", false);

            return Ok(new OkMessage(shapedResources));
        }

        [HttpGet("search", Name = "GetSongsBySearch")]
        public async Task<IActionResult> Search(SongParameters songParameters)
        {
            ValidateMapping<SongResource, Song>(songParameters.OrderBy);
            ValidateFields<SongResource>(songParameters.Fields);
            if (Results.Count != 0) return Results.First();

            var list = await _repository.GetSongsBySearchAsync(songParameters);

            var resources = _mapper.Map<IEnumerable<Song>, IEnumerable<SongResource>>(list);

            var shapedResources = resources.ToDynamicIEnumerable(songParameters.Fields);

            CreateHeader(songParameters, list, "GetSongsBySearch", false);

            return Ok(new OkMessage(shapedResources));
        }

        [HttpGet("{mid}", Name = "GetSongPlayInfo")]
        public async Task<IActionResult> Get(string mid, string fields = null)
        {
            ValidateFields<PlayInfoResource>(fields);
            if (Results.Count != 0) return Results.First();

            var playInfo = await _repository.GetSongPlayInfoAsync(mid);

            if (string.IsNullOrEmpty(playInfo.Vkey))
            {
                return NotFound(new NotFoundResourceMessage
                {
                    Msg = "没有权限播放这首歌"
                });
            }

            var resource = _mapper.Map<PlayInfo, PlayInfoResource>(playInfo);

            var shapedResource = resource.ToDynamic(fields);

            return Ok(new OkMessage(shapedResource));
        }
    }
}
