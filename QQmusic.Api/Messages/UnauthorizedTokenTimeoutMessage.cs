using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine UnauthorizedTokenTimeoutError
     *
     * @apiError UnauthorizedTokenTimeout 请求的header中Authorization里的token已超时.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 401 Unauthorized
     *     {
     *        "code": 401,
     *        "msg": "Token已过期",
     *        "errorCode": 40010,
     *        "data": null
     *     }
     **/
    public class UnauthorizedTokenTimeoutMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status401Unauthorized;
        public override string Msg { get; set; } = "Token已过期";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40010;
    }
}
