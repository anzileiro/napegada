using MongoDB.Bson;
using NaPegada.Model.DTO.Doacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Model
{
    public class MensagemPrivadaMOD : ObjectMongo
    {
        public MensageiroMOD Remetente { get; set; }
        public MensageiroMOD Destinatario { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; }
        public MensagemPrivadaDoacaoMOD Doacao { get; set; }

        public MensagemPrivadaMOD()
        {

        }

        public MensagemPrivadaMOD(MensagemPrivadaDTO dto)
        {
            DataCriacao = DateTime.Now;
            Remetente = dto.Remetente;
            Destinatario = dto.Destinatario;
            Doacao = dto.Doacao;
        }

        public bool EhRequisicaoAdocao()
        {
            return Doacao != null;
        }
    }
}
