using System;
using System.Collections.Generic;
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

        public int DeletarCotacao(Cotacao cotacao) => _db.Delete(cotacao);

        public IEnumerable<Cotacao> GetAllCotacao() => _db.GetCotacoes();

        public Cotacao GetCotacao(decimal codigoCotacao) => _db.GetCotacao(codigoCotacao);

        public bool Save(Cotacao cotacao) => _db.Save(cotacao);
    }
}
