using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace SampleCompany.Infra
{
    public class ValidationActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            var error = (from item in modelState.Values
                             from errorItem in item.Errors
                             select errorItem.ErrorMessage).ToList();

            if (!modelState.IsValid)
                context.Result = new JsonResult(new { error });
        }
    }
}
