using Newtonsoft.Json;

namespace Teste.Models
{
    public class Moeda
    {
        [JsonProperty("simbolo")]
        public string Simbolo { get; set; }
        [JsonProperty("nomeFormatado")]
        public string NomeFormatado { get; set; }
        [JsonProperty("tipoMoeda")]
        public string TipoMoeda { get; set; }
    }
}