using KopkeHome_ModelLayer.DataModel;
using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KopkeHome_WebApp.WebUtility
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public string _Role { get; set; }
        public CustomAuthorize(string Role)
        {
            _Role = Role;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.Get<User>("CurrentUser");
            var Role = _Role.Split(',').ToList();
            var UserType = Constant.GetNames(userId.RoleId);
            var currentrole = Role.Where(x => x.Contains(UserType)).FirstOrDefault();
            if (Role.Where(x => x.Contains(UserType)).FirstOrDefault() != UserType)
                context.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(new { action = "UnAuthorize", controller = "Home" }));
        }
    }


}
