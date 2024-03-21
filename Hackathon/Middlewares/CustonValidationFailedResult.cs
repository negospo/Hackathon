using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hackathon.Middlewares
{
    public class CustonValidationFailedResult : ObjectResult
    {
        public CustonValidationFailedResult(ModelStateDictionary modelState)
            : base(new CustonValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
