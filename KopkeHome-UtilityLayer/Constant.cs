namespace KopkeHome_UtilityLayer
{
#nullable disable
    public class Constant
    {
        //public const string Host = "smtp.gmail.com";
        //public const string Email = "chetuindiauser@gmail.com";
        //public const string Password = "Chetu@97!@#";
        //public const string True = "True";
        //public const string False = "False";


        public const int Contractor = 1;
        public const int SubContractor = 2;
        public const int IndependentContractor = 3;
        public const int HomeOwner = 4;
        public const int Admin = 5;

        public static string GetNames(int code)
        {
            foreach (var field in typeof(Constant).GetFields())
            {
                if ((int)field.GetValue(null) == code)
                    return field.Name.ToString();
            }
            return "";
        }


    }
}
