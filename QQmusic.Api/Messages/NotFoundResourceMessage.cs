using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine NotFoundResourceError
     *
     * @apiError NotFoundResource 找不到请求的资源.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 404 Not Found
     *     {
     *        "code": 404,
     *        "msg": "请求的资源不存在",
     *        "errorCode": 40004,
     *        "data": null
     *     }
     **/
    public class NotFoundResourceMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status404NotFound;
        public override string Msg { get; set; } = "请求的资源不存在";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40004;
    }
}
