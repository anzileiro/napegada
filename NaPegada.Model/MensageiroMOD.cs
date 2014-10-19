using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    //"Perfil" de usuário para envio de mensagens
    public class MensageiroMOD
    {
        public ObjectId IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Reputacao { get; set; }
        public string NomeFotoPerfil { get; set; }
    }
}
