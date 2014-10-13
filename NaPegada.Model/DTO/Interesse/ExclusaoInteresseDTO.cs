using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model.DTO.Interesse
{
    public class ExclusaoInteresseDTO
    {

        public ObjectId IdUsuario { get; set; }
        public ObjectId IdInteresse { get; set; }

    }
}
