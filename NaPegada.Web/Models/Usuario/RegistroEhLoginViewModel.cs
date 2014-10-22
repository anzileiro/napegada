using System.ComponentModel.DataAnnotations;

namespace NaPegada.Web.Models.Usuario
{
    public class RegistroEhLoginViewModel
    {
        [Display(Name = "E-mail:"), 
        Required(ErrorMessage = "Informe seu e-mail"),
        StringLength(100, ErrorMessage = "Limite de caracteres 100")]
        public string Email { get; set; }

        [Display(Name = "Senha:"),
        Required(ErrorMessage = "Informe sua senha"),
        StringLength(100, ErrorMessage = "Limite de caracteres 20")]
        public string Senha { get; set; }

        public string Nome { get; set; }
    }
}