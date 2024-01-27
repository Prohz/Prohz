using System.ComponentModel.DataAnnotations;
using System.Net;

namespace KopkeHome_ModelLayer
{
#nullable disable
    public class Response
    {
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Status Code")]
        public HttpStatusCode Statuscode { get; set; }
        [Display(Name = "Error")]
        public bool Error { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Error Details")]
        public string ErrorDetails { get; set; }
        public Response()
        {
            this.Statuscode = HttpStatusCode.OK;
            this.Status = "Success";
        }
        public object Data { get; set; }
        public int? Code { get; set; }
        public int? WorkStatus { get; set; }
        public bool IsDocumentVerified { get; set; }
    }

}
