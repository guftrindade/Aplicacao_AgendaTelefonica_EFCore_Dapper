using AgendaComDapper.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaComDapper.Repositorio
{
    public class ContatoRepositorio
    {
        private readonly IDbConnection _dbConnection;

        #region Construtor
        public ContatoRepositorio(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        #endregion

        public async Task<Contato> ObterContatoPorId(int id)
        {
            var contato = await _dbConnection.GetAsync<Contato>(id);
            return contato;
        }


        public async Task<Contato> ObterContatoPeloNumeroTelefone(string telefone)
        {
            var comandoSql = "SELECT * FROM Contatos WHERE telefone = @telefone";
            var contato = await _dbConnection.QueryFirstOrDefaultAsync<Contato>(comandoSql, new { telefone });
            return contato;
        }


        public async Task<IList<Contato>> ListarContatos()
        {
            var contatos = await _dbConnection.GetAllAsync<Contato>();
            return contatos.ToList();
        }


        public async Task<bool> Adicionar(Contato contato)
        {
            var result = await _dbConnection.InsertAsync(contato);
            return result > 0;
        }


        public async Task<bool>Atualizar(Contato contato)
        {
            return await _dbConnection.UpdateAsync(contato);
        }


        public async Task<bool>Remover(int idContato)
        {
            return await _dbConnection.DeleteAsync(new Contato() { Id = idContato });
        }
    }
}
