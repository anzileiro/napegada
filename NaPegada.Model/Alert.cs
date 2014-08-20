using System;

namespace NaPegada.Model
{
    public class Alert
    {
        public Action<string, string> Message { get; set; }
    }
}
