using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Model.DTO;
using NaPegada.Repository;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Web.Models.Doacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    public class DoacaoController : BaseAsyncController
    {        
        [HttpGet]
        public async Task<PartialViewResult> Detalhes(string id = null)
        {
            var model = default(DetalhesViewModel);

            if(!string.IsNullOrWhiteSpace(id))
            {
                var userBus = new UsuarioBUS(new UsuarioREP());
                var doacao = await userBus.ObterDoacao(id);
                model = new DetalhesViewModel(doacao);
            }
            else
            {
                model = new DetalhesViewModel();
            }

            return PartialView("_Doacao", model);
        }

        [HttpPost]
        public async Task<ActionResult> Detalhes(DetalhesViewModel model)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());            
            var dto = await ObterDTO(model);
            var ehCadastro = string.IsNullOrWhiteSpace(model.Id);

            if(ehCadastro)
            {
                
                await userBus.RegistrarDoacao(dto);
                TempData["sucesso"] = "Doação cadastrada com sucesso";
            }
            else
            {
                await userBus.AtualizarDoacao(dto);
                TempData["sucesso"] = "Doação atualizada com sucesso";
            }            

            return RedirectToAction("MinhasDoacoes", "Usuario");
        }

        private async Task<RegistroDoacaoDTO> ObterDTO(DetalhesViewModel model)
        {
            var dto = new RegistroDoacaoDTO();
            var user = ObterUsuarioDaSecao();
            var paths = await CarregarFotosNoTemp(model);

            dto.Doacao = model.ConverterParaDoacao();
            dto.IdUsuario = user.Id;

            return dto;
        }

        private async Task<IEnumerable<string>> CarregarFotosNoTemp(DetalhesViewModel model)
        {
            return await Task.Run(() => {
                var paths = new List<string>(); 

                foreach (var foto in model.Fotos)
                    paths.Add(CarregarFotoNoTemp(foto));

                return paths;
            });            
        }

        private string CarregarFotoNoTemp(HttpPostedFileBase foto)
        {
            var path = string.Format("{0}-{1}{2}", Server.MapPath("~/Arquivos/temp/doacao"), Guid.NewGuid(), Path.GetExtension(foto.FileName));

            using (var fileStream = System.IO.File.Create(path))
                foto.InputStream.CopyTo(fileStream);

            return path;
        }

        [HttpGet]
        public async Task<PartialViewResult> Exclusao(string id)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());
            var doacao = await userBus.ObterDoacao(id);

            return PartialView("_DeletarDoacao", new ExclusaoViewModel(doacao));
        }

        [HttpPost]
        public async Task<ActionResult> Excluir(string id)
        {
            var userBus = new UsuarioBUS(new UsuarioREP());

            await userBus.ExcluirDoacao(ObterDTO(id));
            TempData["sucesso"] = "Doação excluída com sucesso";

            return RedirectToAction("MinhasDoacoes", "Usuario");
        }

        private ExclusaoDoacaoDTO ObterDTO(string id)
        {
            var dto = new ExclusaoDoacaoDTO();

            dto.IdDoacao = ObjectId.Parse(id);
            dto.IdUsuario = ObterUsuarioDaSecao().Id;

            return dto;
        }        
	}
}