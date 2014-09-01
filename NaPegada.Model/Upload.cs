using System.Web;

namespace NaPegada.Model
{
    public class Upload
    {
        public HttpPostedFileBase Arquivo { get; set; }
        public string Nome { get; set; }
    }
}
