using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QQmusic.Api.Messages
{
    public interface IMessage<T>
    {
        int Code { get; set; }
        T Msg { get; set; }
        int ErrorCode { get; set; }
        object Data { get; set; }
        string ToJson();
    }
}
