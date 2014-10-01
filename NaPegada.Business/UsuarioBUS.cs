using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Repository;
using System.Threading.Tasks;
using System.Web;

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

        public async Task Registrar(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha);
            await _usuarioREP.Registrar(usuarioMOD);
        }

        public async Task<bool> EhUsuario(UsuarioMOD usuarioMOD)
        {
            usuarioMOD.Senha = _utilitaria.CriptografarSenha(usuarioMOD.Senha);
            return await _usuarioREP.EhUsuario(usuarioMOD);
        }

        public async Task<UsuarioMOD> ObterPorEmail(string email)
        {
            return await _usuarioREP.ObterPorEmail(email);
        }

        public async Task<UsuarioMOD> ObterPorId(string id)
        {
            return await _usuarioREP.ObterPorId(ConverterParaObjectId(id));
        }

        public async Task Atualizar(UsuarioMOD usuarioMOD, HttpPostedFileBase arquivo)
        {
            if (arquivo != null)
            {
                usuarioMOD.NomeFotoPerfil = _utilitaria.VerificaEhSalvaArquivo(arquivo, @"~/Content/upload/usuario").Result;
            }
            else
            {
                usuarioMOD.NomeFotoPerfil = usuarioMOD.NomeFotoPerfil;
            }
            await _usuarioREP.Atualizar(usuarioMOD);
        }

        public ObjectId ConverterParaObjectId(string s)
        {
            return ObjectId.Parse(s);
        }
    }
}
