using MongoDB.Bson;
using System;
using System.IO;
using System.Threading.Tasks;
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

        public async Task<string> VerificaEhSalvaArquivo(HttpPostedFileBase arquivo, string caminho)
        {
            string retorno = string.Empty;
            if (arquivo != null)
            {
                retorno = Salvar(arquivo, caminho);
            }
            return await Task.Run(() => retorno);
        }
        public ObjectId ConverterParaObjectId(string s)
        {
            return ObjectId.Parse(s);
        }

        public string CriptografarSenha(string senha)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "sha1");
        }

    }
}
