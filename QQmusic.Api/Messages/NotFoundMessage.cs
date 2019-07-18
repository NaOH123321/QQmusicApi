using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine NotFoundError
     *
     * @apiError NotFound 请求的控制器或是方法不存在.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 404 Not Found
     *     {
     *        "code": 404,
     *        "msg": "控制器或方法不存在",
     *        "errorCode": 40000,
     *        "data": null
     *     }
     **/
    public class NotFoundMessage :Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status404NotFound;
        public override string Msg { get; set; } = "控制器或方法不存在";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40000;
    }
}
