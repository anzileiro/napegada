using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class ItemRelatorioDoacaoMOD
    {
        private IList<string> _fotos;

        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<string> Fotos 
        {
            get { return _fotos; }
            protected set { _fotos = (IList<string>)value; }
        }

        public ItemRelatorioDoacaoMOD()
        {
            _fotos = new List<string>();
        }

        public void AdicionarFoto(string foto)
        {
            if (string.IsNullOrWhiteSpace(foto))
                throw new ArgumentNullException("foto");

            _fotos.Add(foto);
        }
    }
}
