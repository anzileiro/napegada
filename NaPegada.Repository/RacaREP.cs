using NaPegada.DataAccess;
using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace NaPegada.Repository
{
    public class RacaREP
    {

        private Conexao<RacaMOD> _conn;


        public async Task<List<RacaMOD>> BuscarPorEspecie(AnimalEspecie especie)
        {

            var listaDoBanco = default(IQueryable<RacaMOD>);

            using (_conn = new Conexao<RacaMOD>())
            {

                listaDoBanco = await Task.Run(() => _conn.Conectar("mongodb://localhost", "napegada", "usuario").AsQueryable<RacaMOD>().Where(r => r.Especie == especie));          

                

            }

            return listaDoBanco.ToList();
        }

    }
}
