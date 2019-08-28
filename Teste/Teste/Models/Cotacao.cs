using SQLite;
using System;

namespace Teste.Models
{
    public class Cotacao
    {
        [PrimaryKey, AutoIncrement]
        public int CodigoCotacap { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime DataCotacao { get; set; }
    }
}