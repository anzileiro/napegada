using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class AnimalIdadeMOD : ObjectMongo
    {
        public ushort? Anos { get; set; }
        public ushort? Meses { get; set; }
    }
}
