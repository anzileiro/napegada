using MongoDB.Bson;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace NaPegada.Business
{
    public class Utility
    {
        public Action<string, string> Message { get; set; }
        private const string passphrase = "SHIELD";

        private string Save(HttpPostedFileBase file, string path)
        {
            var name = FormatNameFile(file);
            file.SaveAs(Path.Combine(HostingEnvironment.MapPath(path), name));
            return name;
        }

        private string FormatNameFile(HttpPostedFileBase name)
        {
            return (Guid.NewGuid() + Path.GetExtension(name.FileName)).ToLower().ToString();
        }

        public string VerifyAndSaveFile(HttpPostedFileBase file, string path)
        {
            if (file != null)
                return Save(file, path);

            return string.Empty;
        }
        public ObjectId ConvertToId(string s)
        {
            return ObjectId.Parse(s);
        }

        public string Crypt(string s)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
                TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
                desalg.Key = deskey;
                desalg.Mode = CipherMode.ECB;
                desalg.Padding = PaddingMode.PKCS7;
                byte[] encrypt_data = utf8.GetBytes(s);
                var encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
            }
            return Convert.ToBase64String(results);
        }

        public string Decrypt(string s)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
                var desalg = new TripleDESCryptoServiceProvider();
                desalg.Key = deskey;
                desalg.Mode = CipherMode.ECB;
                desalg.Padding = PaddingMode.PKCS7;
                byte[] decrypt_data = Convert.FromBase64String(s);
                var decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            return utf8.GetString(results);
        }

    }
}
