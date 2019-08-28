using System;
using System.Collections.Generic;
using System.Text;
using Teste.Models;

namespace Teste.Repositories
{
    public interface ICotacaoRepository
    {
        bool Save(Cotacao cotacao);
        IEnumerable<Cotacao> GetAll();
    }
}