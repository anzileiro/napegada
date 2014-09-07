using NaPegada.Model;
using NaPegada.Repository;

namespace NaPegada.Business
{
    public class UsuarioBUS
    {
        private readonly UsuarioREP _usuarioREP;
        private readonly Utility _utility;

        public UsuarioBUS(Utility utility_)
        {
            _usuarioREP = new UsuarioREP();
            _utility = utility_;
        }


        public void Registrar(UsuarioMOD usuarioMOD)
        {
            try
            {
                usuarioMOD.Senha = _utility.CriptografarSenha(usuarioMOD.Senha, "sha1");
                _usuarioREP.Registrar(usuarioMOD);
                _utility.Mensagem("sucesso", "Registro efetuado com sucesso !");
            }
            catch
            {
                _utility.Mensagem("erro", "Não foi possível efetuar o registro !");
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
            return _usuarioREP.ObterPorId(_utility.ConverterParaObjectId(id));
        }

        public void Atualizar(UsuarioMOD usuarioMOD, string id)
        {
            if (usuarioMOD.ArquivoFotoPerfil.Arquivo != null)
            {
                usuarioMOD.NomeFotoPerfil = _utility.VerificaEhSalvaArquivo(usuarioMOD.ArquivoFotoPerfil.Arquivo, @"~/Content/upload/usuario");
            }

            _usuarioREP.Atualizar(usuarioMOD, _utility.ConverterParaObjectId(id));
        }


    }
}
