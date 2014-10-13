using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model.DTO.Doacao
{
    public class ExclusaoDoacaoDTO
    {
        public ObjectId IdUsuario { get; set; }
        public ObjectId IdDoacao { get; set; }
    }
}
