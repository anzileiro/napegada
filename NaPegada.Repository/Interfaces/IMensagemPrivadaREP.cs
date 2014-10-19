using NaPegada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaPegada.Repository.Interfaces
{
    public interface IMensagemPrivadaREP
    {
        Task Registrar(MensagemPrivadaMOD mensagem);
    }
}
