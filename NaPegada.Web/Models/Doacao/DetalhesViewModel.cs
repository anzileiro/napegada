using MongoDB.Bson;
using NaPegada.Model;
using NaPegada.Model.DTO;
using NaPegada.Model.DTO.Doacao;
using NaPegada.Web.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NaPegada.Web.Models.Doacao
{
    public class DetalhesViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage="Nome é obrigatório")]
        public string Nome { get; set; }

        public string Raca { get; set; }

        [Required(ErrorMessage="Espécie é obrigatória")]
        public AnimalEspecie? Especie { get; set; }

        [Required(ErrorMessage="Porte é obrigatório")]
        public AnimalPorte? Porte { get; set; }

        [Range(0, 50, ErrorMessage="Anos válidos: 0 ~ 50")]
        public ushort? Anos { get; set; }

        [Range(0, 11, ErrorMessage="Meses válidos: 0 ~ 11")]
        public ushort? Meses { get; set; }

        public bool EhVacinado { get; set; }
        public bool EhCastrado { get; set; }
        public bool TomouVermifugo { get; set; }
        public IEnumerable<HttpPostedFileBase> Fotos { get; set; }

        public DetalhesViewModel(DoacaoMOD doacao)
        {
            Id = doacao.ToString();
            Nome = doacao.NomeAnimal;
            Raca = doacao.RacaAnimal;
            Especie = doacao.EspecieAnimal;
            Porte = doacao.PorteAnimal;
            Anos = doacao.IdadeAnimal == null || !doacao.IdadeAnimal.Anos.HasValue ? null : (ushort?) doacao.IdadeAnimal.Anos.Value;
            Meses = doacao.IdadeAnimal == null || !doacao.IdadeAnimal.Meses.HasValue ? null : (ushort?) doacao.IdadeAnimal.Meses.Value;
            EhVacinado = doacao.EhVacinado;
            EhCastrado = doacao.EhCastrado;
            TomouVermifugo = doacao.TomouVermifugo;
        }

        public DetalhesViewModel()
        {

        }

        public async Task<RegistroDoacaoDTO> ConverterParaRegistroDoacaoDTO(ObjectId userId)
        {
            var dto = new RegistroDoacaoDTO();

            dto.IdUsuario = userId;
            dto.Doacao = await Task.Run(() => ObterDoacao());

            return dto;
        }        

        private DoacaoMOD ObterDoacao()
        {
            var doacao = new DoacaoMOD();

            doacao.Id = string.IsNullOrWhiteSpace(Id) ? ObjectId.GenerateNewId() : ObjectId.Parse(Id);
            doacao.NomeAnimal = Nome;
            doacao.RacaAnimal = Raca;
            doacao.EspecieAnimal = Especie.Value;
            doacao.PorteAnimal = Porte.Value;
            
            if(Anos.HasValue)
            {
                doacao.IdadeAnimal = new AnimalIdadeMOD();
                doacao.IdadeAnimal.Anos = Anos;
            }

            if(Meses.HasValue && doacao.IdadeAnimal != null)
            {
                doacao.IdadeAnimal.Meses = Meses;
            }

            doacao.EhVacinado = EhVacinado;
            doacao.EhCastrado = EhCastrado;
            doacao.TomouVermifugo = TomouVermifugo;
            var fotos = ObterFotos();
            fotos.ForEach(foto => doacao.AdicionarFoto(foto));

            return doacao;
        }

        private List<string> ObterFotos()
        {
            var fotos = new List<string>();

            foreach(var arquivo in Fotos)
            {
                var path = Salvar(arquivo);
                fotos.Add(path);
            }

            return fotos;
        }

        private string Salvar(HttpPostedFileBase arquivo)
        {
            var pathFotos = "~/Arquivos/Fotos/Doacoes/";
            var fileName = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(arquivo.FileName));
            var fullPath = string.Format("{0}{1}", HostingEnvironment.MapPath(pathFotos), fileName);

            using (var fileStream = File.Create(fullPath))
                arquivo.InputStream.CopyTo(fileStream);

            return string.Format("{0}{1}", pathFotos, fileName);
        }
    }
}