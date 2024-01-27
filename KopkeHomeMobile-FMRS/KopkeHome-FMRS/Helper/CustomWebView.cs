using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KopkeHome_FMRS.Helper;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Helper
{
   public class CustomWebView:WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(string), typeof(CustomWebView), default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public static readonly BindableProperty IsPDFProperty = BindableProperty.Create(propertyName: "IsPDF",
               returnType: typeof(bool),
               declaringType: typeof(CustomWebView),
               defaultValue: default(bool));

        public bool IsPDF
        {
            get { return (bool)GetValue(IsPDFProperty); }
            set { SetValue(IsPDFProperty, value); }
        }

        public static readonly BindableProperty IsPDFProperty1 = BindableProperty.Create(propertyName: "IsPDF1",
               returnType: typeof(bool),
               declaringType: typeof(CustomWebView),
               defaultValue: default(bool));

        public bool IsPDF1
        {
            get { return (bool)GetValue(IsPDFProperty1); }
            set { SetValue(IsPDFProperty1, value); }
        }
    }
}
