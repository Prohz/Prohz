using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.ViewModels
{
    public class LegalFilesViewModel
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public IFormFile LegalDocFiles { get; set; }
        public string PrevFile { get; set; }
    }
}
