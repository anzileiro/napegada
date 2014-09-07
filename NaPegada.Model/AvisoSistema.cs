using System;

namespace NaPegada.Model
{
    public class AvisoSistema
    {
        public Action<string, string> Mensagem { get; set; }
    }
}
