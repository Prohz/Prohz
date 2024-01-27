using System.Text;

namespace KopkeHome_WebApp.WebUtility
{
    public class Common
    {
        private IWebHostEnvironment Environment;

        public Common(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        /// <summary>
        ///  This method takes multiple iformfile files and saves it into server.
        ///  This method returns file name/path in string.
        /// </summary>
        public string MultiplFiles(List<IFormFile> Files)
        {

            string SingleFile = string.Empty;

            StringBuilder sb = new StringBuilder();
            if (Files != null)
            {
                foreach (var File in Files)
                {
                    SingleFile = SaveFile(File);

                    sb.AppendLine(SingleFile + ",");

                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///Saves IFormFile files into server.
        ///This method returns file name/path in string.
        /// </summary>
        public string SaveFile(IFormFile file)
        {
            try
            {
                string Encryptedfilename = string.Empty;
                if (file != null)
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    //var filePath = Path.Combine(_config["StoredFilesPath"], 
                    //Path.GetRandomFileName());

                    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = Path.GetFileName(file.FileName);
                    Encryptedfilename = Guid.NewGuid() + fileName;
                    using (FileStream stream = new FileStream(Path.Combine(path, Encryptedfilename), FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }


                }
                return Encryptedfilename;
            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        ///  This method checks hyper text transfer protocol of a link.
        /// </summary>
        public string CheckHttp(string Http)
        {
            if (string.IsNullOrEmpty(Http))
            {
                return string.Empty;
            }

            string output = string.Empty;
            if (Http.Length > 3)
            {
                string str = Http.Substring(0, 4).ToLower();

                if (str == "http" /*|| str == "www."*/)
                {
                    output = Http;
                }
                else
                {
                    output = "https://" + Http;
                }
                return output;
            }

            output = "https://" + Http;
            return output;



        }

        /// <summary>
        ///  This method deletes files from server.
        ///  This method returns boolean.
        /// </summary>
        public bool DeleteFile(string Filename)
        {
            bool IsDelted = true;

            if (!string.IsNullOrEmpty(Filename))
            {
                List<string> docs = Filename.Replace("\r\n", "").Split(',').Reverse().ToList();
                foreach (var dox in docs)
                {
                    if (!string.IsNullOrEmpty(dox))
                    {
                        string filePath = Path.Combine(this.Environment.WebRootPath, "Uploads", dox);
                        if(File.Exists(filePath)) 
                        {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                        }
                        IsDelted = true;
                    }

                }


            }
            return IsDelted;
        }
    }
}
