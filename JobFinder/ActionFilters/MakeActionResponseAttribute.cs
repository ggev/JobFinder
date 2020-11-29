using JobFinder.Application.Dtos.ServiceResultModels;
using JobFinder.Domain.Entities;
using JobFinder.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace JobFinder.ActionFilters
{
    public class MakeActionResponseFilter : IActionFilter
    {
        private readonly IRepository _repo;

        public MakeActionResponseFilter(IRepository repo)
        {
            _repo = repo;
        }

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
            var guestId = context.HttpContext.Request.Headers.Where(x => x.Key == "userId").SelectMany(x => x.Value).FirstOrDefault();
            if (guestId != default)
            {
                if (!_repo.FilterAsNoTracking<User>(x => x.IdentityKey == guestId).Any())
                {
                    _repo.Create(new User {IdentityKey = guestId});
                    _repo.SaveChanges();
                }
            }
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