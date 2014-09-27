using NaPegada.Model;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NaPegada.Web.Models
{
    public class UsuarioViewModel
    {

        public UsuarioViewModel() { }

        public UsuarioViewModel(UsuarioMOD usuarioMOD)
        {
            if (usuarioMOD.Endereco == null)
                usuarioMOD.Endereco = new EnderecoMOD();

            Id = usuarioMOD.Id.ToString();
            Nome = usuarioMOD.Nome;
            Email = usuarioMOD.Email;
            Senha = usuarioMOD.Senha;
            NomeFotoPerfil = usuarioMOD.NomeFotoPerfil;
            Cep = usuarioMOD.Endereco.Cep;
            Logradouro = usuarioMOD.Endereco.Logradouro;
            Uf = usuarioMOD.Endereco.Uf;
            Localidade = usuarioMOD.Endereco.Localidade;
            Bairro = usuarioMOD.Endereco.Bairro;
            Numero = usuarioMOD.Endereco.Numero;
            Complemento = usuarioMOD.Endereco.Complemento;
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NomeFotoPerfil { get; set; }
        public bool ManterConectado { get; set; }

        //[Required(ErrorMessage = "Informe seu CEP.")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Uf { get; set; }
        public string Localidade { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }
        public int Numero { get; set; }
        public HttpPostedFileBase Upload { get; set; }
    }
}