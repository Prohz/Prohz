using KopkeHome_ModelLayer.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_UtilityLayer.CustomMiddleware
{
    public static class JwtHelper
    {

        public static IEnumerable<Claim> GetClaims(UserViewModel userViewModel, Guid Id)
        {
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim(ClaimTypes.Email, userViewModel.Email),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(UserViewModel userViewModel, out Guid Id)
        {
            Id = Guid.NewGuid();
            return GetClaims(userViewModel, Id);
        }
        public static string GenTokenkey(UserViewModel userViewModel, Jwt jwt)
        {
            try
            {
                // var UserToken = new UserTokens();
                if (userViewModel == null) throw new ArgumentException(nameof(userViewModel));
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwt.IssuerSigningKey);
                Guid Id = Guid.Empty;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                // UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwt.ValidIssuer, audience: jwt.ValidAudience, claims: GetClaims(userViewModel, out Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                var Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                //UserToken.UserName = model.UserName;
                //UserToken.Id = model.Id;
                //UserToken.GuidId = Id;
                return Token;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
