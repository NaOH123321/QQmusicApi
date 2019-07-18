using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Helpers;
using Microsoft.AspNetCore.Http;

namespace QQmusic.Api.Messages
{
    /**
     * @apiDefine UnprocessableEntityError
     *
     * @apiError UnprocessableEntity 参数校验没有通过，校验结果请参考例子.
     *
     * @apiErrorExample Error-Response
     *     HTTP/1.1 422 Unprocessable Entity
     *     {
     *        "code": 422,
     *        "msg":  {
     *              "userName": [
     *                  {
     *                      "validatorKey": "maxlength",
     *                      "message": "用户名的最大长度是50"
     *                  }
     *              ]
     *        },
     *        "errorCode": 40003,
     *        "data": null
     *     }
     **/
    public class UnprocessableEntityMessage : Message<object>
    {
        public override int Code { get; set; } = StatusCodes.Status422UnprocessableEntity;
        public override object Msg { get; set; }
        public override int ErrorCode { get; set; } = ErrorCodeStatus.ErrorCode40003;
    }
}
