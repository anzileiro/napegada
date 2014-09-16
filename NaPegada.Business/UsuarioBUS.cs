using NaPegada.Model;
using NaPegada.Repository;
using System.Collections.Generic;

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
            return _usuarioREP.ObterPorId(_utilitaria.ConverterParaObjectId(id));
        }

        public void Atualizar(UsuarioMOD usuarioMOD, string id)
        {
            //if (usuarioMOD.ArquivoFotoPerfil.Arquivo != null)
            //{
            //    usuarioMOD.NomeFotoPerfil = _utilitaria.VerificaEhSalvaArquivo(usuarioMOD.ArquivoFotoPerfil.Arquivo, @"~/Content/upload/usuario");
            //}

            _usuarioREP.Atualizar(usuarioMOD, _utilitaria.ConverterParaObjectId(id));
        }

        public IEnumerable<PesquisaMOD> Pesquisar(string dadosPesquisa)
        {
            return _usuarioREP.Pesquisar(dadosPesquisa);
        }
    }
}
