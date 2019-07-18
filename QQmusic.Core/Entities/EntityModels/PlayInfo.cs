using System;
using System.Collections.Generic;
using System.Text;

namespace QQmusic.Core.Entities.EntityModels
{
    public class PlayInfo
    {
        public int Expiration { get; set; }
        public string Filename { get; set; }
        public string Vkey { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
    }
}
