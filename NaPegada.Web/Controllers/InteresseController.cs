using MongoDB.Bson;
using NaPegada.Business;
using NaPegada.Model.DTO.Interesse;
using NaPegada.Web.Models.Interesse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NaPegada.Web.Controllers
{
    [AutenticarAutorizar]
    public class InteresseController : BaseAsyncController
    {
        
        [HttpGet]
        public async Task<PartialViewResult> Detalhes(string id = null)
        {

            var model = default(DetalhesViewModel);

            if (!string.IsNullOrWhiteSpace(id))
            {

                var userBus = new UsuarioBUS();
                var interesse = await userBus.ObterInteresse(id);
                model = new DetalhesViewModel(interesse);

            }
            else
            {

                model = new DetalhesViewModel();

            }


            return PartialView("_Interesse", model);
        }

        [HttpPost]
        public async Task<ActionResult> Detalhes(DetalhesViewModel model)
        {

            var userBus = new UsuarioBUS();
            var dto = ObterDTO(model);
            var ehCadastro = string.IsNullOrWhiteSpace(model.Id);

            if (ehCadastro)
            {
                await userBus.RegistrarInteresse(dto);
                TempData["sucesso"] = "Interesse cadastrado com sucesso";
            }
            else
            {
                await userBus.AtualizarInteresse(dto);
                TempData["sucesso"] = "Interesse atualizado com sucesso";
            }

            return RedirectToAction("MeusInteresses", "Usuario");
        }

        private RegistroInteresseDTO ObterDTO(DetalhesViewModel model)
        {

            var dto = new RegistroInteresseDTO();
            var user = ObterUsuarioDaSecao();

            dto.Interesse = model.ConverterParaInteresse();
            dto.IdUsuario = user.Id;

            return dto;

        }

        [HttpGet]
        public async Task<PartialViewResult> Exclusao(string id)
        {

            var userBus = new UsuarioBUS();
            var interesse = await userBus.ObterInteresse(id);

            return PartialView("_DeletarInteresse", new ExclusaoViewModel(interesse));
        }

        [HttpPost]
        public async Task<ActionResult> Excluir(string id)
        {
            var userBus = new UsuarioBUS();

            await userBus.ExcluirInteresse(ObterDTO(id));

            TempData["sucesso"] = "Interesse deletado com sucesso";

            return RedirectToAction("MeusInteresses", "Usuario");

        }

        private ExclusaoInteresseDTO ObterDTO(string id)
        {
            var dto = new ExclusaoInteresseDTO();

            dto.IdInteresse = ObjectId.Parse(id);
            dto.IdUsuario = ObterUsuarioDaSecao().Id;

            return dto;
        }

    }
}