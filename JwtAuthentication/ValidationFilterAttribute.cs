using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JwtAuthentication
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var ret = new BaseResponseModel();
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                ret.IsSuccess = false;
                ret.ErrorMessages = errors;
                context.Result = new JsonResult(ret);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
