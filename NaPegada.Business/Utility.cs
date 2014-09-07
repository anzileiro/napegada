using MongoDB.Bson;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

namespace NaPegada.Business
{
    public class Utility
    {
        private const string passphrase = "SHIELD";

        private string Salvar(HttpPostedFileBase arquivo, string caminho)
        {
            var name = FormatarNomeDoArquivo(arquivo);
            arquivo.SaveAs(Path.Combine(HostingEnvironment.MapPath(caminho), name));
            return name;
        }

        private string FormatarNomeDoArquivo(HttpPostedFileBase name)
        {
            return (Guid.NewGuid() + Path.GetExtension(name.FileName)).ToLower().ToString();
        }

        public string VerificaEhSalvaArquivo(HttpPostedFileBase arquivo, string caminho)
        {
            if (arquivo != null)
                return Salvar(arquivo, caminho);

            return string.Empty;
        }
        public ObjectId ConverterParaObjectId(string s)
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

        public string CriptografarSenha(string senha, string tipoCriptografia)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(senha, tipoCriptografia);
        }

    }
}
