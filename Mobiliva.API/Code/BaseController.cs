using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Mobiliva.API.Code
{
    [ValidateModel]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]

    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        public BaseController()
        {

        }

        public Guid CurrentUserID
        {
            get
            {
                return Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
            }

        }

        private string GetClaim(string ClaimName)
        {
            return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimName).Value;
        }
    }
}
