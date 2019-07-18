using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine BadRequestFieldsError
     *
     * @apiError BadRequestFields 请求的参数fields找不到相应的字段.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 400 Bad Request
     *     {
     *        "code": 400,
     *        "msg": "不能找到相应的字段塑形",
     *        "errorCode": 40008,
     *        "data": null
     *     }
     **/
    public class BadRequestFieldsMessage : Message<string>
    {
        public override int Code { get; set; } = StatusCodes.Status400BadRequest;
        public override string Msg { get; set; } = "不能找到相应的字段塑形";
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40008;
    }
}
