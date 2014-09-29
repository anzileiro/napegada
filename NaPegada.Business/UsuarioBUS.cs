using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Repository;
using System.Web;
using System.Web.Security;

namespace NaPegada.Business
{
    public class UsuarioBUS : AvisoSistema
    {
        private readonly UsuarioREP _usuarioREP;
        private readonly Utilitaria _utilitaria;

        public UsuarioBUS()
        {
            _usuarioREP = new UsuarioREP();
            _utilitaria = new Utilitaria();
        }

        public void Registrar(UsuarioMOD usuarioMOD)
        {
            try
            {
                usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha, "sha1");
                _usuarioREP.Registrar(usuarioMOD);

                Mensagem("sucesso", "Registro efetuado com sucesso !");
            }
            catch
            {
                Mensagem("erro", "Não foi possível efetuar o registro !");
            }

        }

        public bool EhUsuario(UsuarioMOD usuarioMOD)
        {
            return _usuarioREP.EhUsuario(usuarioMOD);
        }

        public UsuarioMOD ObterPorEmail(string email)
        {
            return _usuarioREP.ObterPorEmail(email);
        }

        public UsuarioMOD ObterPorId(string id)
        {
            return _usuarioREP.ObterPorId(ConverterParaObjectId(id));
        }

        public void Atualizar(UsuarioMOD usuarioMOD, HttpPostedFileBase arquivo)
        {
            try
            {
                if (arquivo != null)
                    usuarioMOD.NomeFotoPerfil = _utilitaria.VerificaEhSalvaArquivo(arquivo, @"~/Content/upload/usuario");
                else
                    usuarioMOD.NomeFotoPerfil = usuarioMOD.NomeFotoPerfil;

                _usuarioREP.Atualizar(usuarioMOD);

                Mensagem("sucesso", "Seu perfil foi atualizado!");
            }
            catch
            {
                Mensagem("erro", "Não foi possível atualizar seu perfil!");
            }


        }

        public bool Logar(UsuarioMOD usuarioMOD)
        {
            //usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha, "sha1");
            //if (EhUsuario(usuarioMOD))
            //{
            //    FormsAuthentication.Authenticate(usuarioMOD.Email, usuarioMOD.Senha);
            //    FormsAuthentication.SetAuthCookie(usuarioMOD.Email, manterCookie);
            //    return true;
            //}
            //return false;

            return false;
        }

        public void Deslogar()
        {
            FormsAuthentication.SignOut();
        }

        public ObjectId ConverterParaObjectId(string s)
        {
            //return ObjectId.Parse(s);

            return ObjectId.Empty;
        }

    }
}
