using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class InteresseMOD: ObjectMongo
    {
        private IList<AnimalPorte> _portes;

        public bool EhVacinado { get; set; }

        public bool TomouVermifugo { get; set; }

        public bool EhCastrado { get; set; }
        public IEnumerable<AnimalPorte> Porte 
        {
            get { return _portes; }
            protected set { _portes = (IList<AnimalPorte>)value; }
        }
        public AnimalEspecie Especie { get; set; }

        public RacaMOD Raca { get; set; }
        public ushort? IdadeMinimaEmAnos { get; set; }
        public ushort? IdadeMaximaEmAnos { get; set; }

        public InteresseMOD()
        {
            _portes = new List<AnimalPorte>();
        }

        public void AdicionarPorte(AnimalPorte porte)
        {
            _portes.Add(porte);
        }
    }
}
