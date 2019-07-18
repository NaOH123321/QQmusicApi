using System;
using System.Collections.Generic;
using System.Text;

namespace QQmusic.Infrastructure.Resources
{
    public class SongResource
    {
        public SongResource()
        {
            Singers = new HashSet<SingerResource>();
        }

        public string Id { get; set; }
        public int Rank { get; set; }
        public int AlbumId { get; set; }
        public string AlbumMid { get; set; }
        public string AlbumName { get; set; }
        public string AlbumPic { get; set; }
        public int DurationTime { get; set; }
        public int SongId { get; set; }
        public string SongMid { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
        public PlayInfoResource PlayInfo { get; set; }
        public ICollection<SingerResource> Singers { get; set; }
    }
}
