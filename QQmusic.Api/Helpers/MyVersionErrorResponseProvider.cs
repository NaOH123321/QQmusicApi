using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace QQmusic.Api.Helpers
{
    public class MyVersionErrorResponseProvider : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            return new BadRequestObjectResult(new BadRequestVersionMessage());
        }
    }
}
