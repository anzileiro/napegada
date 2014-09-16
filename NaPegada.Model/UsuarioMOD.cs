using System.Collections.Generic;
using System;

namespace NaPegada.Model
{
    public class UsuarioMOD : ObjectMongo
    {
        private IList<TelefoneMOD> _telefones;
        private IList<MensagemPrivadaMOD> _mensagensEnviadas;
        private IList<MensagemPrivadaMOD> _mensagensRecebidas;
        private IList<DoacaoMOD> _doacoes;

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Reputacao { get; set; }
        public string NomeFotoPerfil { get; set; }
        public InteresseMOD Interesse { get; set; }
        public EnderecoMOD Endereco { get; set; }
        public IEnumerable<TelefoneMOD> Telefones 
        {
            get { return _telefones; } 
            protected set {_telefones = (IList<TelefoneMOD>) value;} 
        }
        public IEnumerable<MensagemPrivadaMOD> MensagensEnviadas 
        {
            get { return _mensagensEnviadas; }
            protected set { _mensagensEnviadas = (IList<MensagemPrivadaMOD>)value; }
        }
        public IEnumerable<MensagemPrivadaMOD> MensagensRecebidas 
        {
            get { return _mensagensRecebidas; }
            protected set { _mensagensRecebidas = (IList<MensagemPrivadaMOD>)value; }
        }
        public IEnumerable<DoacaoMOD> Doacoes 
        {
            get { return _doacoes; }
            protected set { _doacoes = (IList<DoacaoMOD>)value; }
        }

        public UsuarioMOD()
        {
            _telefones = new List<TelefoneMOD>();
            _mensagensEnviadas = new List<MensagemPrivadaMOD>();
            _mensagensRecebidas = new List<MensagemPrivadaMOD>();
            _doacoes = new List<DoacaoMOD>();
        }

        public void AdicionarTelefone(TelefoneMOD telefone)
        {
            if (telefone == null)
                throw new ArgumentNullException("telefone");

            _telefones.Add(telefone);
        }

        public void AdicionarMensagemEnviada(MensagemPrivadaMOD mensagem)
        {
            if (mensagem == null)
                throw new ArgumentNullException("mensagem");

            _mensagensEnviadas.Add(mensagem);
        }

        public void AdicionarMensagemRecebida(MensagemPrivadaMOD mensagem)
        {
            if (mensagem == null)
                throw new ArgumentNullException("mensagem");

            _mensagensRecebidas.Add(mensagem);
        }

        public void AdicionarDoacao(DoacaoMOD doacao)
        {
            if (doacao == null)
                throw new ArgumentNullException("doacao");

            _doacoes.Add(doacao);
        }
    }
}
