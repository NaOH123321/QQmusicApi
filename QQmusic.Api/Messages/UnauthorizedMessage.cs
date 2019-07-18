using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine UnauthorizedError
     *
     * @apiError Unauthorized 需要在请求的header中加入Authorization.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 401 Unauthorized
     *     {
     *        "code": 401,
     *        "msg": "没有权限",
     *        "errorCode": 40001,
     *        "data": null
     *     }
     **/
    public class UnauthorizedMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status401Unauthorized;
        public override string Msg { get; set; } = "没有权限";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40001;
    }
}
