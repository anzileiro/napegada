using NaPegada.DataAccess;
using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace NaPegada.Repository
{
    public class RacaREP
    {

        private Conexao<RacaMOD> _conn = new Conexao<RacaMOD>();

        public async Task<IEnumerable<string>> BuscarPorEspecie(AnimalEspecie especie)
        {
            var racas = new List<RacaMOD>();

            racas = await Task.Run(() => racas = _conn.Conectar("mongodb://localhost", "napegada", "raca")
                                                      .FindAs<RacaMOD>(Query<RacaMOD>.EQ(_ => _.Especie, especie))
                                                      .SetFields(Fields<RacaMOD>.Include(_ => _.Nome)).ToList());

            return racas.Select(_ => _.Nome);
        }

    }
}
