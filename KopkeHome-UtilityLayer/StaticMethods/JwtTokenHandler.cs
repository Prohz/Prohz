using System.IdentityModel.Tokens.Jwt;

namespace KopkeHome_UtilityLayer.StaticMethods
{
    public static class JwtTokenHandler
    {
        public static string GetUserName(string token)
        {
            try
            {
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var jti = tokenS.Claims.First(claim => claim.Type == "Email").Value;
                var Firstname = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
                var Lastname = tokenS.Claims.First(claim => claim.Type == "LastName").Value;
                var sec = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
                string fullName = Firstname + "" + Lastname;

                return fullName;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static string GetUserEmail(string token)
        {
            try
            {
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var Email = tokenS.Claims.First(claim => claim.Type == "Email").Value;


                return Email;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public static string GetUserRole(string token)
        {
            try
            {
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var UserRole = tokenS.Claims.First(claim => claim.Type == "Role").Value;


                return UserRole;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
