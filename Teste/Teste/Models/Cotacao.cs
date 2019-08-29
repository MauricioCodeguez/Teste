using SQLite;
using System;

namespace Teste.Models
{
    public class Cotacao
    {
        [PrimaryKey, AutoIncrement]
        public int CodigoCotacao { get; set; }
        public string SimboloMoeda { get; set; }
        public string NomeFormatado { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime DataCotacao { get; set; }

        public string DescricaoMoeda { get => $"{NomeFormatado} - {SimboloMoeda}"; }
    }
}