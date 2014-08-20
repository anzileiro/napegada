using System.Collections.Generic;

namespace NaPegada.Model
{
    public sealed class UserMOD : Object
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool StayConnected { get; set; }
        public Upload Upload { get; set; }
        public ICollection<AnimalMOD> Animal { get; set; }
    }
}
