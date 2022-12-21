using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mobiliva.Model;
using System.Linq;
using Mobiliva.Model.Response;

namespace Mobiliva.API.Code
{
    public class ValidateModel : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var response = new BaseResponse<bool>();

                response.ValidationErrors = context.ModelState.Select(x => new ValidationError
                {
                    Key = x.Key,
                    Value = x.Value.Errors.FirstOrDefault().ErrorMessage
                }).ToList();
                response.SetMessage("Lütfen hatalı alanları kontrol edin.");

                foreach (var item in response.ValidationErrors)
                {
                    item.Key = item.Key.ToLower();
                }

                context.Result = new OkObjectResult(response);
            }
        }
    }
}
