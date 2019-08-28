using System;
using System.Collections.Generic;
using System.Text;
using Teste.Models;

namespace Teste.Repositories
{
    public class CotacaoRepository : ICotacaoRepository
    {
        private readonly Database.Database _db;

        public CotacaoRepository()
        {
            _db = new Database.Database();
        }

        public IEnumerable<Cotacao> GetAll() => _db.GetCotacoes();

        public bool Save(Cotacao cotacao) => _db.Save(cotacao);
    }
}
