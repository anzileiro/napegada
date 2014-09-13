using NaPegada.Model;
using System.Collections.Generic;

namespace NaPegada.Web.Models
{
    public class UsuarioViewModel
    {
        public UsuarioMOD Usuario { get; set; }
        public IEnumerable<PesquisaMOD> ResultadoPesquisa { get; set; }
    }
}