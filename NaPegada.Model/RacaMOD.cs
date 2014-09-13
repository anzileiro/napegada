using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class RacaMOD: ObjectMongo
    {
        
        public string Nome { get; set; }

        public IEnumerable<AnimalEspecie> Especie { get; set; }

    }
}
