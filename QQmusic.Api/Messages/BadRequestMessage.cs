using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine BadRequestError
     *
     * @apiError BadRequest 请求的参数有错误.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 400 Bad Request
     *     {
     *        "code": 400,
     *        "msg": "参数错误",
     *        "errorCode": 40006,
     *        "data": null
     *     }
     **/
    public class BadRequestMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status400BadRequest;
        public override string Msg { get; set; } = "参数错误";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40006;
    }
}
