using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class MensagemPrivadaDoacaoMOD
    {
        public ObjectId IdDoacao { get; set; }
        public string NomeAnimal { get; set; }
    }
}
