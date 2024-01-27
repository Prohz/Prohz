using KopkeHome_ModelLayer.DataModel;
using KopkeHome_UtilityLayer.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KopkeHome_WebApp.WebUtility
{
    public class ValidateSession : ActionFilterAttribute
    {
        public ValidateSession() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.Get<User>("CurrentUser");

            if (userId == null)
                context.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "signin", controller = "user" }));
        }

    }
}
