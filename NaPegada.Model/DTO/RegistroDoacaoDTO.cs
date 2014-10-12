using MongoDB.Bson;
using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model.DTO
{
    public class RegistroDoacaoDTO
    {
        public DoacaoMOD Doacao { get; set; }
        public ObjectId IdUsuario { get; set; }
    }
}
