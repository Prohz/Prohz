using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ServiceHelper
{
   public class APIHttpHelper
    {
        public const string Bearer = "Bearer";
        public const string ApplicationJsonText = "application/json";
        //public static string BaseUrl = "http://174.129.63.207:81/";
        //public static string ImagesBaseUrl = "http://174.129.63.207/Uploads/";
        public static string BaseUrl = "https://uatapi.prohz.net/";
        public static string ImagesBaseUrl = "https://uatapi.prohz.net/Uploads/";
        public static string serviceUrl = "Account/GetUserByEmail";

    }
}
