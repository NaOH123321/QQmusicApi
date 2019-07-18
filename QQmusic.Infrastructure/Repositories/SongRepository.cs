using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQmusic.Core.Entities;
using QQmusic.Infrastructure.Helpers;
using QQmusic.Infrastructure.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QQmusic.Core.Entities.EntityModels;

namespace QQmusic.Infrastructure.Repositories
{
    public class SongRepository
    {
        private const string uid = "876576457";//"126548448";

        private const string cid = "205361747"; //"205361747"; 

        private const string TopUrl =
            @"https://c.y.qq.com/v8/fcg-bin/fcg_v8_toplist_cp.fcg?g_tk=5381&uin=0&format=json&inCharset=utf-8&outCharset=utf-8¬ice=0&platform=h5&needNewCode=1&tpl=3&page=detail&type=top&topid=27&_=1519963122923";

        public async Task<PaginatedList<Song>> GetAllAsync(SongParameters parameters)
        {
            var json = await HttpHelper.HttpGetAsync(TopUrl, contentType: "application/json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);
            var jsonArray = (JArray) jObject["songlist"];

            var songList = new Collection<Song>();

            var countList = new List<int>();
            for (var i = 0; i < jsonArray.Count; i++)
            {
                countList.Add(i);
            }
            var takeList = countList.Skip(parameters.PageIndex * parameters.PageSize).Take(parameters.PageSize);

            foreach (var i in takeList)
            {
                var data = (JObject) jsonArray[i]["data"];
                var song = await GetSongAsync(data);
                song.Rank = int.Parse(jsonArray[i]["cur_count"].ToString());

                songList.Add(song);
            }

            return new PaginatedList<Song>(parameters.PageIndex, parameters.PageSize, countList.Count, songList);
        }

        private async Task<Song> GetSongAsync(JObject data, bool withPlayInfo = true)
        {
            var song = new Song
            {
                Id = Guid.NewGuid().ToString(),
                AlbumId = int.Parse(data["albumid"].ToString()),
                AlbumMid = data["albummid"].ToString(),
                AlbumName = data["albumname"].ToString(),
                Interval = int.Parse(data["interval"].ToString()),
                SongId = int.Parse(data["songid"].ToString()),
                SongMid = data["songmid"].ToString(),
                SongName = data["songname"].ToString()
            };

            if (data.ContainsKey("songurl"))
            {
                song.SongUrl = data["songurl"].ToString();
            }
            var singers = (JArray) data["singer"];
            var singerList = new Collection<Singer>();
            for (int j = 0; j < singers.Count; j++)
            {
                var singer = new Singer
                {
                    Id = singers[j]["id"].ToString(),
                    Mid = singers[j]["mid"].ToString(),
                    Name = singers[j]["name"].ToString()
                };
                if (!string.IsNullOrEmpty(singer.Mid))
                {
                    singer.Pic = $@"https://y.gtimg.cn/music/photo_new/T001R300x300M000{singer.Mid}.jpg";
                }

                singerList.Add(singer);
            }
            if (!string.IsNullOrEmpty(song.AlbumMid))
            {
                song.AlbumPic = $@"https://y.gtimg.cn/music/photo_new/T002R300x300M000{song.AlbumMid}.jpg";
            }
            song.Singers = singerList;
            if (withPlayInfo)
            {
                if (song.Interval != 0)
                {
                    song.PlayInfo = await GetSongPlayInfoAsync(song.SongMid, song.SongUrl);
                }
                else
                {
                    song.PlayInfo = new PlayInfo
                    {
                        Expiration = 0,
                        Filename = "",
                        Vkey = "",
                        Url = ""
                    };
                }
            }

            return song;
        }

        private async Task<ValueTuple<int, string, string, string>> GetTokenAsync(string songmid, string songfilename)
        {
            try
            {
                var token_url =
                    $@"https://c.y.qq.com/base/fcgi-bin/fcg_music_express_mobile3.fcg?format=json&platform=yqq&cid={cid}&songmid={songmid}&filename={songfilename}&guid={uid}";
                var json = await HttpHelper.HttpGetAsync(token_url, contentType: "application/json");
                var jObject = (JObject) JsonConvert.DeserializeObject(json);

                var ip = jObject["userip"].ToString();
                var expiration = int.Parse(jObject["data"]["expiration"].ToString());
                var jsonArray = (JArray) jObject["data"]["items"];
                var filename = jsonArray[0]["filename"].ToString();
                var vkey = jsonArray[0]["vkey"].ToString();

                return (expiration, filename, vkey, ip);
            }
            catch (Exception)
            {
                return (0, "songmid:"+ songmid, "", "");
            }

        }

        public async Task<PlayInfo> GetSongPlayInfoAsync(string songmid, string songUrl = null)
        {
            var songfilename = "";
            if (!string.IsNullOrEmpty(songmid))
            {
                songfilename = $"C400{songmid}.m4a";
            }

            var (expiration, filename, vkey, ip) = await GetTokenAsync(songmid, songfilename);
            var play_url =
                $@"http://ws.stream.qqmusic.qq.com/{filename}?fromtag=0&guid={uid}&vkey={vkey}";

            var playInfo = new PlayInfo
            {
                Expiration = expiration,
                Filename = filename,
                Vkey = vkey,
                Url = play_url,
                Ip = ip
            };
            return playInfo;
        }

        public async Task<PaginatedList<Song>> GetSongsBySearchAsync(SongParameters songParameters)
        {
            var search_url =
                $@"https://c.y.qq.com/soso/fcgi-bin/client_search_cp?aggr=1&cr=1&flag_qc=0&format=json&p={songParameters.PageIndex + 1}&w={songParameters.Keywords}";
            var json = await HttpHelper.HttpGetAsync(search_url, contentType: "application/json");
            var jObject = (JObject) JsonConvert.DeserializeObject(json);
            var jsonData = (JObject) jObject["data"];
            var jsonSong = (JObject) jsonData["song"];
            var currentPage = int.Parse(jsonSong["curpage"].ToString());
            var pageSize = int.Parse(jsonSong["curnum"].ToString());
            var totalCounts = int.Parse(jsonSong["totalnum"].ToString());

            var jsonArray = (JArray) jsonSong["list"];

            var songList = new Collection<Song>();
            for (var i = 0; i < jsonArray.Count; i++)
            {
                var song = await GetSongAsync((JObject) jsonArray[i]);
                songList.Add(song);
            }

            return new PaginatedList<Song>(currentPage, pageSize, totalCounts, songList);
        }

        //public void Add(Song song)
        //{

        //    //_myContext.Add(person);
        //}

        //public void Delete(Song person)
        //{
        //    _myContext.Persons.Remove(person);
        //}

        //public void Update(Person person)
        //{
        //    _myContext.Entry(person).State = EntityState.Modified;
        //}
    }
}
