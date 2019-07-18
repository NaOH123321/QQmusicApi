using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    public class CreatedMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status201Created;
        public override string Msg { get; set; } = "创建成功";
        public override int ErrorCode { get; set; }

        public CreatedMessage(object value)
        {
            Data = value;
        }
    }
}
