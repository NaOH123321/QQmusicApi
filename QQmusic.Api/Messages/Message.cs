using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QQmusic.Api.Messages
{
    public abstract class Message<T> :IMessage<T>
    {
        public abstract int Code { get; set; }
        public abstract T Msg { get; set; }
        public abstract int ErrorCode { get; set; }
        public object Data { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                Code,
                Msg,
                ErrorCode,
                Data
            }, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
