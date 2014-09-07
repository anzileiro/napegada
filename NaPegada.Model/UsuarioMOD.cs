using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace NaPegada.Model
{
    public class UsuarioMOD : ObjectMongo
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Reputacao { get; set; }
        public bool MantenhaMeConectado { get; set; }
        public Upload ArquivoFotoPerfil { get; set; }
        public string NomeFotoPerfil { get; set; }
        public EnderecoMOD Endereco { get; set; }
        public IEnumerable<TelefoneMOD> Telefones { get; set; }
    }
}
