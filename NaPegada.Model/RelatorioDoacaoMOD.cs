using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class RelatorioDoacaoMOD
    {
        private IList<ItemRelatorioDoacaoMOD> _itens;

        public DateTime DataInicio { get; set; }
        public AdotanteMOD Adotante { get; set; }
        public IEnumerable<ItemRelatorioDoacaoMOD> Itens 
        {
            get { return _itens; }
            protected set { _itens = (IList<ItemRelatorioDoacaoMOD>)value; }
        }

        public RelatorioDoacaoMOD()
        {
            _itens = new List<ItemRelatorioDoacaoMOD>();
        }

        public void AdicionarItem(ItemRelatorioDoacaoMOD item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _itens.Add(item);
        }
    }
}
