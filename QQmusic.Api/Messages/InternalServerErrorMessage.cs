using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine InternalServerError
     *
     * @apiError InternalServerError 服务器端出现错误.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 500 Internal Server Error
     *     {
     *        "code": 500,
     *        "msg": "服务器错误",
     *        "errorCode": 999,
     *        "data": null
     *     }
     **/
    public class InternalServerErrorMessage : Message<string>
    {
        public override int Code { get; set; }= StatusCodes.Status500InternalServerError;
        public override string Msg { get; set; } = "服务器错误";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode999;
    }
}
