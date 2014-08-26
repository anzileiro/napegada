using System.Collections.Generic;
using NaPegada.Utility.Messages;
using System.ComponentModel.DataAnnotations;
using System;

namespace NaPegada.Model
{
    public sealed class UserMOD : Object
    {
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesUTL), ErrorMessageResourceName = "Required")]
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool StayConnected { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public Roles Role { get; set; }
        public Upload Upload { get; set; }
        public string NameFile { get; set; }
        public ICollection<AnimalMOD> Animal { get; set; }
    }
}
