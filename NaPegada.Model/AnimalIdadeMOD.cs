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

        public override string ToString()
        {
            var valor = string.Empty;

            if(Anos.HasValue && Meses.HasValue)
            {
                valor = string.Format("{0} {1} e {2} {3}", Anos, ObterStringAnos(), Meses, ObterStringMeses());
            }
            else if(Anos.HasValue)
            {
                valor = string.Format("{0} {1}", Anos, ObterStringAnos());
            }

            return valor;
        }

        private string ObterStringAnos()
        {
            var valor = string.Empty;

            if(Anos.HasValue)
            {
                valor = (Anos == 1) ? "ano" : "anos";
            }

            return valor;
        }

        private string ObterStringMeses()
        {
            var valor = string.Empty;

            if (Meses.HasValue)
            {
                valor = (Meses == 1) ? "mês" : "meses";
            }

            return valor;
        }
    }
}
