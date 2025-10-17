using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Gerenciador_Pessoal.Models;

namespace Gerenciador_Pessoal.Data
{
    public class BancoDados
    {
        readonly SQLiteAsyncConnection _database;

        public BancoDados(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transacao>().Wait();
        }

        public Task<List<Transacao>> GetTransacoesAsync()
        {
            return _database.Table<Transacao>().ToListAsync();
        }

        public Task<int> SaveTransacaoAsync(Transacao transacao)
        {
            if (transacao.Id != 0)
                return _database.UpdateAsync(transacao);
            else
                return _database.InsertAsync(transacao);
        }

        public Task<int> DeleteTransacaoAsync(Transacao transacao)
        {
            return _database.DeleteAsync(transacao);
        }
    }
}
