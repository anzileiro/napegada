using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class InteresseMOD: ObjectMongo
    {
        public string Caracteristicas { get; set; }

        public bool EhVacinado { get; set; }

        public bool TomouVermifugo { get; set; }

        public bool EhCastrado { get; set; }
        public IEnumerable<AnimalPorte> Porte { get; set; }
        public IEnumerable<AnimalEspecie> Especie { get; set; }

        public RacaMOD raca { get; set; }

    }
}
