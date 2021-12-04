using AgendaComDapper.InputModel;
using AgendaComDapper.Models;
using AgendaComDapper.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaComDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly ContatoRepositorio _contatoRepositorio;

        #region Construtor
        public ContatosController(IDbConnection dbConnection)
        {
            _contatoRepositorio = new ContatoRepositorio(dbConnection);
        }
        #endregion

        #region AdicionarContato
        [HttpPost]
        public async Task<IActionResult> Adicionar(ContatoInput dadosEntrada)
        {
            var contato = new Contato
            {
                Nome = dadosEntrada.Nome,
                Apelido = dadosEntrada.Apelido,
                Telefone = dadosEntrada.Telefone,
                Email = dadosEntrada.Email
            };

            var contatoAdicionado = await _contatoRepositorio.Adicionar(contato);
            if (contatoAdicionado)
                return Ok(contato);

            return StatusCode(500, "Contato não pode ser adicionado!");
        }
        #endregion

        #region ListarContatos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var contatos = await _contatoRepositorio.ListarContatos();
            if (!contatos.Any())
                return NotFound("Nenhum registro foi encontrato!");

            return Ok(contatos);
        }
        #endregion

        #region AtualizarContato
        [HttpPut]
        public async Task<IActionResult>Atualizar(AtualizarContatoInput dadosEntrada)
        {
            var contato = await _contatoRepositorio.ObterContatoPorId(dadosEntrada.Id);
            if (contato == null)
                return NotFound("Contato não localizado!");

            contato.Nome = dadosEntrada.Nome;
            contato.Apelido = dadosEntrada.Apelido;
            contato.Telefone = dadosEntrada.Telefone;
            contato.Email = dadosEntrada.Email;

            var contatoAtualizado = await _contatoRepositorio.Atualizar(contato);
            if (contatoAtualizado)
                return Ok(contato);

            return StatusCode(500, "Contato não atualizado!");
        }
        #endregion

        #region DeletarContato
        [HttpDelete]
        public async Task<IActionResult>Deletar(int id)
        {
            var contato = await _contatoRepositorio.ObterContatoPorId(id);
            if (contato == null)
                return NotFound("Contato não foi encontrato!");

            var contatoRemovido = await _contatoRepositorio.Remover(id);
            if (contatoRemovido)
                return Ok("Contato removido com sucesso!");

            return StatusCode(500, "Contato não removido!");
        }
        #endregion
    }
}
