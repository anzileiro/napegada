using System.Collections.Generic;
using NaPegada.Utility.Messages;
using System.ComponentModel.DataAnnotations;
using System;

namespace NaPegada.Model
{
    public class UserMOD : Object
    {
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesUTL), ErrorMessageResourceName = "Required")]
        public string Mail { get; set; }
        public string Senha { get; set; }
        public string FotoPerfil { get; set; }
        public int Reputacao { get; set; }
        public IEnumerable<TelefoneMOD> Telefones { get; set; }

    }
}
