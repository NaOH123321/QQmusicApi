using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QQmusic.Core.Entities;
using QQmusic.Infrastructure.Resources;

namespace QQmusic.Api.AutoMapper
{
    public class SongProfile : Profile
    {
        public SongProfile()

        {
            CreateMap<Song, SongResource>()
                .ForMember(dest => dest.DurationTime,
                    opt => opt.MapFrom(src => src.Interval));
        }
    }
}
