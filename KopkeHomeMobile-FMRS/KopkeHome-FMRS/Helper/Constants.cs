namespace KopkeHome_FMRS.Helper
{
    public static class Constants
    {
        //public static string BaseUrl = "http://174.129.63.207:81/";
         //public static string ImagesBaseUrl = "http://174.129.63.207/Uploads/";
        
        //-----------Staging Url--------------//
        public static string BaseUrl = "https://uatapi.prohz.net/";
        public static string ImagesBaseUrl = "https://uatapi.prohz.net/Uploads/";

        //-----------Production Url--------------//
        // public static string BaseUrl = "https://www.prohz.net/";
        // public static string ImagesBaseUrl = "https://www.prohz.net/Uploads/";
        public const string AccountSignIn = "Account/SignIn";
        public static string GetUserByEmailId = "Account/GetUserByEmail";
        public const string GetMyPost = "posts/myposts?type=";
        public const string Review = "&review=";
        public const string GetRandomPost = "posts?type=";
        public const string GetUserId= "Account/GetUserByEmail";
        public const string GetUserById ="Account/GetUserById";
        public const string GetStateList = "Account/GetStates";
        public const string GetCityList = "Account/GetCitiesList";
        public static string ForgotPassword = "Account/ForgotPassword";
        public static string ResetPassword = "Account/ResetPassword";
        public static string AcoountUpdateBasicInfo = "Account/UpdateBasicInfo";
        public static string GetZipCodeList= "Dashboard/GetZipCodeList";
        public static string GetCategoryList="Dashboard/GetCategoriesList";
        public static string SearchContractorList = "Dashboard/SearchContractorsList";
        public static string AccountWorkStatus="Account/UserWorkStatus";
        public static string ContractorReview = "Dashboard/ContractorsReview";
        public static string GetActivePlanDetail= "Membership/GetCustomPlanDetailsByUserId";
        public static string GetBusinessProfileDetail = "Dashboard/GetContractorProfileDetails";
        public static string GetZipCodeListByCityName = "Membership/GetZipCodesByCityName";
        //String Constant
        public const string LanguageMode = "EN/ES";
        public const string ApplicationType = "application/json";
        public const string ResetPasswordToken = "ResetPasswordKey_Token";
        public const string Alert = "Alert";
        public const string Success = "Successful";
        public const string ID = "ID";
        public const string ContractorId = "ContractorId";
        public const string ContractorAccountType = "ContractorAccountType";
        public const string RoleID = "RoleID";
        public const string OK = "Ok";
        public const string Yes= "Yes";
        public const string No = "No";
        public const string FirstName = "firstname";
        public const string LastName = "lasttname";
        public const string UserID = "UserId";
        public const string ContractorMemberId = "MemeberId";
        public const string UserMemebrId = "UserMemeberId";
        public const string WorkStatus = "workstatus";
        public const string Email = "email";
        public const string UserProfilePic = "userprofilePic";
        public const string Password = "password";
        public const string IsToggled = "toggle";
        public const string True = "True";
        public const string False = "False";
        public const string NotAvailable = "Not Available";
        public const string StateDefaultEntry = "State Name";
        public const string AppToken = "AppToken";
        public const string WorkStatusChanged=  "StatusChanged";
        public const string CurrentLanguage = "Language";
        public const string ENLanguage = "EN";
        public const string enLanguageCode = "en";
        public const string enUSCode="en-US";
        public const string ESLanguage = "ES";
        public const string esSVCode="es-SV";
        public const string ImageFormat= ".png";
        public static string token { get; set; }
        //Image Constant
        public static string NotCheckBoxImage = "unchecked_checkbox.png";
        public static string GreenCheckBoximage = "green_checkbox.png";
        public static string YellowCheckBoximage = "yellow_checkbox.png";      
        public static string RedCheckBoximage = "red_checkbox.png";      
        public static string Green = "Booking for Now(0-4 weeks)";
        public static string Yellow = "Booking for Soon(5-16 weeks)";
        public static string Red = "Booking for Later(17-52 weeks)";
        public static string Available = "available.png";
        public static string Appoint = "appoint.png";
        public static string Booked = "booked.png";
        public const string Close_Eye_Icon = "close_eye_icon";
        public const string Eye_Icon = "eye";
        public const string DisLikeImageRed = "dislike_red_fifty";
        public const string DisLikeImageGray = "dislike_gray_fifty";
        public const string LikeImageGreen = "greenlike_icon.png";
        public const string LikeImageGray = "like_gray_fifty";
        public const string DefaultProfilePic = "defaultprofile.png";
        public const string ProfilePicBlue="profileblue";
        public const string DockIcon = "dockicon";      

        //regex
        public static string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";
        public static string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    }
}
