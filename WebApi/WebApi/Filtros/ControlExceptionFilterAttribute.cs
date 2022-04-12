using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Filtros
{
    public class ControlExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ControlExceptionFilterAttribute> _logger;
        public ControlExceptionFilterAttribute(ILogger<ControlExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                context.Exception.Message,
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);
            context.Result = result;
        }
    }
}
