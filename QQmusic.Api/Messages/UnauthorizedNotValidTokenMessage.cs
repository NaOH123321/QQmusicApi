using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine UnauthorizedNotValidTokenError
     *
     * @apiError UnauthorizedNotValidToken 请求的header中Authorization里的token不是合法的token.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 401 Unauthorized
     *     {
     *        "code": 401,
     *        "msg": "无效Token",
     *        "errorCode": 40009,
     *        "data": null
     *     }
     **/
    public class UnauthorizedNotValidTokenMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status401Unauthorized;
        public override string Msg { get; set; } = "无效Token";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40009;
    }
}
