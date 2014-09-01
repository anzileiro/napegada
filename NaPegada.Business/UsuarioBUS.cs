using NaPegada.Model;
using NaPegada.Repository;
using System;

namespace NaPegada.Business
{
    public class UsuarioBUS : Utility
    {
        private readonly UsuarioREP _usuarioREP;

        public UsuarioBUS()
        {
            _usuarioREP = new UsuarioREP();
        }

        public void Registrar(UsuarioMOD userMOD)
        {
            _usuarioREP.Registrar(userMOD);
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
            usuarioMOD.FotoPerfil.Nome = VerificaEhSalvaArquivo(usuarioMOD.FotoPerfil.Arquivo, @"~/Content/upload/usuario");
            _usuarioREP.Atualizar(usuarioMOD, ConverterParaObjectId(id));
        }

    }
}
