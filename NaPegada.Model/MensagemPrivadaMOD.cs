using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class MensagemPrivadaMOD
    {
        public MensageiroMOD Remetente { get; set; }
        public MensageiroMOD Destinatario { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
