using Newtonsoft.Json;
using System.Collections.Generic;
using Teste.Enum;

namespace Teste.Classes
{
    public class ServiceResult<T> where T : class
    {
        [JsonProperty("value")]
        public IEnumerable<T> Data { get; set; }
        public ServiceResultStatus Status { get; set; }
    }
}