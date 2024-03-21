using Microsoft.AspNetCore.Mvc.Filters;

namespace Hackathon.Middlewares
{
    public class CustonValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new CustonValidationFailedResult(context.ModelState);
            }
        }
    }
}
