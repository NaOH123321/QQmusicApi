using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Core.Entities.EntityModels;

namespace QQmusic.Core.Entities
{
    public class Song : Entity
    {
        public Song()
        {
            Singers = new HashSet<Singer>();
        }

        public int Rank { get; set; }
        public int AlbumId { get; set; }
        public string AlbumMid { get; set; }
        public string AlbumName { get; set; }
        public string AlbumPic { get; set; }
        public int Interval { get; set; }
        public int SongId { get; set; }
        public string SongMid { get; set; }
        public string SongName { get; set; }
        public string SongUrl { get; set; }
        public PlayInfo PlayInfo { get; set; }
        public ICollection<Singer> Singers { get; set; }
    }
}
