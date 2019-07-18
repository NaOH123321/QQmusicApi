using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine ForbiddenError
     *
     * @apiError Forbidden 请求的权限级别不够.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 403 Forbidden
     *     {
     *        "code": 403,
     *        "msg": "权限不够",
     *        "errorCode": 40020,
     *        "data": null
     *     }
     **/
    public class ForbiddenMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status403Forbidden;
        public override string Msg { get; set; } = "权限不够";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40020;
    }
}
