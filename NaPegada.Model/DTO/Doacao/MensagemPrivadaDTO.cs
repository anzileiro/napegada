using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model.DTO.Doacao
{
    public class MensagemPrivadaDTO
    {
        public MensageiroMOD Destinatario { get; set; }
        public MensagemPrivadaDoacaoMOD Doacao { get; set; }
        public MensageiroMOD Remetente { get; set; }
    }
}
