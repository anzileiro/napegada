using System.Collections.Generic;

namespace NaPegada.Model
{
    public class PessoaJuridicaMOD : UsuarioMOD
    {
        private IList<EnderecoMOD> _enderecos;

        public TipoPJ Tipo { get; set; }
        public ulong CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string Site { get; set; }
        public IEnumerable<EnderecoMOD> Enderecos 
        {
            get { return _enderecos; }
            protected set { _enderecos = (IList<EnderecoMOD>)value; }
        }

        public PessoaJuridicaMOD() : base()
        {
            _enderecos = new List<EnderecoMOD>();
        }
    }
}
