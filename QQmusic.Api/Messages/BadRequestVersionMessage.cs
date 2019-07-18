using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine BadRequestVersionError
     *
     * @apiError BadRequest 请求的版本号不存在.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 400 Bad Request
     *     {
     *        "code": 400,
     *        "msg": "版本号不存在",
     *        "errorCode": 40011,
     *        "data": null
     *     }
     **/
    public class BadRequestVersionMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status400BadRequest;
        public override string Msg { get; set; } = "版本号不存在";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40011;
    }
}
