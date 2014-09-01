using NaPegada.Model;
using NaPegada.Repository;

namespace NaPegada.Business
{
    public class UsuarioBUS : Utility
    {
        private readonly UsuarioREP _usuarioREP;

        public UsuarioBUS()
        {
            _usuarioREP = new UsuarioREP();
        }

        public void Registrar(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = CriptografarSenha(usuarioMOD.Senha, "sha1");
            _usuarioREP.Registrar(usuarioMOD);
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

        public void Atualizar(UsuarioMOD usuarioMOD, string id)
        {
            usuarioMOD.NomeFotoPerfil = VerificaEhSalvaArquivo(usuarioMOD.ArquivoFotoPerfil.Arquivo, @"~/Content/upload/usuario");
            _usuarioREP.Atualizar(usuarioMOD, ConverterParaObjectId(id));
        }

    }
}
