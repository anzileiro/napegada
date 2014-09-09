using NaPegada.Model;
using NaPegada.Repository;
using NaPegada.Util;

namespace NaPegada.Business
{
    public class UsuarioBUS : AvisoSistema, IInjecao<UsuarioREP, Utilitaria>
    {
        private UsuarioREP _usuarioREP;
        private Utilitaria _utilitaria;

        public UsuarioBUS()
        {
            this.Injetar(new UsuarioREP(), new Utilitaria());
        }
        public void Injetar(UsuarioREP usuarioREP_, Utilitaria utilitaria_)
        {
            this._usuarioREP = usuarioREP_;
            this._utilitaria = utilitaria_;
        }

        public void Registrar(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha, "sha1");
            _usuarioREP.Registrar(usuarioMOD);

            //try
            //{
            //    usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha, "sha1");
            //    _usuarioREP.Registrar(usuarioMOD);
            //    Mensagem("sucesso", "Registro efetuado com sucesso !");
            //}
            //catch
            //{
            //    Mensagem("erro", "Não foi possível efetuar o registro !");
            //}

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
            return _usuarioREP.ObterPorId(_utilitaria.ConverterParaObjectId(id));
        }

        public void Atualizar(UsuarioMOD usuarioMOD, string id)
        {
            if (usuarioMOD.ArquivoFotoPerfil.Arquivo != null)
            {
                usuarioMOD.NomeFotoPerfil = _utilitaria.VerificaEhSalvaArquivo(usuarioMOD.ArquivoFotoPerfil.Arquivo, @"~/Content/upload/usuario");
            }

            _usuarioREP.Atualizar(usuarioMOD, _utilitaria.ConverterParaObjectId(id));
        }



       
    }
}
