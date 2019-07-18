using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine UnsupportedMediaTypeError
     *
     * @apiError UnsupportedMediaType 请求的header中Content-Type不是支持的类型.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 415 Unsupported Media Type
     *     {
     *        "code": 415,
     *        "msg": "不支持的MediaType",
     *        "errorCode": 40002,
     *        "data": null
     *     }
     **/
    public class UnsupportedMediaTypeMessage :Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status415UnsupportedMediaType;
        public override string Msg { get; set; } = "不支持的MediaType";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40002;
    }
}
