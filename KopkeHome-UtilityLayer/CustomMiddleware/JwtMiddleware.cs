using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_UtilityLayer.CustomMiddleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAccount userService, JwtValidateToken jWTValidateToken)
        {
            var token = context.Request.Cookies["token"]; //context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var email = jWTValidateToken.ValidateToken(token);
            if (email != null)
            {
                // attach user to context on successful jwt validation
                var userDetails = await userService.GetByEmail(email).ConfigureAwait(false);
                context.Items["User"] = userDetails;
            }
            await _next(context);
        }
    }
}
