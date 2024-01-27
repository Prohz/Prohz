using System.Security.Cryptography;
using System.Text;

namespace KopkeHome_UtilityLayer
{
    public class EncryptDecrypt

    {
        string cipherText = "AQAAAAEAACcQAAAAELxwF6qTCcxg6Gd0+fX7iCfNH0Ese3SrNHaspIcpG7f+lKKl5K7tYsDCeeP794kNQw==";
        public string DecryptString(string cipherText)
        {
            string EncryptionKey = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Mode = CipherMode.CBC;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public string EncryptString(string clearText)
        {
            string EncryptionKey = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string MD5Hash(string text)
        {
            XSystem.Security.Cryptography.MD5 md5 = new XSystem.Security.Cryptography.MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }


    }
}
