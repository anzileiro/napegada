using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model.DTO.Doacao
{
    public class AdocaoDTO
    {
        public ObjectId IdDoacao { get; set; }
        public UsuarioMOD Adotante { get; set; }

        public AdocaoDTO(string idDoacao, UsuarioMOD adotante)
        {
            IdDoacao = ObjectId.Parse(idDoacao);
            Adotante = adotante;
        }
    }
}
