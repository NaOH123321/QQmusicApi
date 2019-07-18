using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine NotAcceptableError
     *
     * @apiError NotAcceptable 请求的header中Accept不是支持的类型.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 406 Not Acceptable
     *     {
     *        "code": 406,
     *        "msg": "不支持的Acceptable",
     *        "errorCode": 40005,
     *        "data": null
     *     }
     **/
    public class NotAcceptableMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status406NotAcceptable;
        public override string Msg { get; set; } = "不支持的Acceptable";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40005;
    }
}
