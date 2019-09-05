using Newtonsoft.Json;
using System.Collections.Generic;

namespace Teste.Classes
{
    public class ServiceResult<T> where T : class
    {
        [JsonProperty("value")]
        public IEnumerable<T> Data { get; set; }
    }
}