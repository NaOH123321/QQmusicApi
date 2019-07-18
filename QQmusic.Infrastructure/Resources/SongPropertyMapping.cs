using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Core.Entities;
using QQmusic.Infrastructure.Services;

namespace QQmusic.Infrastructure.Resources
{
    public class SongPropertyMapping : PropertyMapping<SongResource, Song>
    {
        public SongPropertyMapping() : base(
            new Dictionary<string, List<MappedProperty>>(StringComparer.OrdinalIgnoreCase)
            {
                [nameof(SongResource.Rank)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.Rank)}
                },
                [nameof(SongResource.AlbumId)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.AlbumId)}
                },
                [nameof(SongResource.AlbumMid)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.AlbumMid)}
                },
                [nameof(SongResource.AlbumName)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.AlbumName)}
                },
                [nameof(SongResource.AlbumPic)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.AlbumPic)}
                },
                [nameof(SongResource.DurationTime)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.Interval)}
                },
                [nameof(SongResource.SongId)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.SongId)}
                },
                [nameof(SongResource.SongMid)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.SongMid)}
                },
                [nameof(SongResource.SongName)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.SongName)}
                },
                [nameof(SongResource.SongUrl)] = new List<MappedProperty>()
                {
                    new MappedProperty() {Name = nameof(Song.SongUrl)}
                },
            })
        {
        }
    }
}
