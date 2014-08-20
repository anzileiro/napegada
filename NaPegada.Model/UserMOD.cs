using System.Collections.Generic;
using NaPegada.Utility.Messages;
using NaPegada.Utility.Regex;
using System.ComponentModel.DataAnnotations;

namespace NaPegada.Model
{
    public sealed class UserMOD : Object
    {
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesUTL), ErrorMessageResourceName = "Required")]
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool StayConnected { get; set; }
        public Upload Upload { get; set; }
        public ICollection<AnimalMOD> Animal { get; set; }
    }
}
