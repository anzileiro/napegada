using MongoDB.Bson;
using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

namespace NaPegada.Business
{
    public class Utilitaria
    {
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

        public string CriptografarSenha(string senha, string tipoCriptografia)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(senha, tipoCriptografia);
        }

    }
}
