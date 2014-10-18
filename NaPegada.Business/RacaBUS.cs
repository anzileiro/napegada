using NaPegada.Model;
using NaPegada.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Business
{
    public class RacaBUS : AvisoSistema
    {

        private readonly RacaREP _racaREP;

        public RacaBUS()
        {

            _racaREP = new RacaREP();

        }


        public async Task<IEnumerable<string>> BuscarPorEspecie(AnimalEspecie especie)
        {
            return await _racaREP.BuscarPorEspecie(especie);
        }

    }
}
