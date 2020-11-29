using JobFinder.Application.Dtos.ServiceResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace JobFinder.ActionFilters
{
    public class MakeActionResponseAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = new ServiceResult();
            if (context.Exception == default)
            {
                if (!(context.Result is ObjectResult) && !(context.Result is EmptyResult))
                    return;
                var objectResult = !(context.Result is EmptyResult) ? (ObjectResult)context.Result : null;
                result.Data = objectResult?.Value;
                result.Success = true;
                result.Messages.AddMessage(MessageType.Info, "Ok");
            }
            else
            {
                result.Messages.AddMessage(MessageType.Error, context.Exception.Message);
                if (context.Exception.InnerException != default)
                    result.Messages.AddMessage(MessageType.Error, context.Exception.InnerException.Message);
                result.Success = false;
                context.Exception = default;
            }

            context.Result = new JsonResult(result);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            var result = new ServiceResult();
            result.Messages.AddMessage(MessageType.Error,
                string.Join("; ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            result.Success = false;
            context.Result = new JsonResult(result);
        }
    }
}