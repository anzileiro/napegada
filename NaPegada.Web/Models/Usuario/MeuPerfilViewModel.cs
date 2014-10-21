using System.Web;

namespace NaPegada.Web.Models.Usuario
{
    public sealed class MeuPerfilViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public HttpPostedFileBase ArquivoFoto { get; set; }
    }
}