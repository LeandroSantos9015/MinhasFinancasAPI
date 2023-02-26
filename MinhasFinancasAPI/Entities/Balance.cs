using MinhasFinancasAPI.Util.Enumerator;
using System.Text.Json.Serialization;

namespace MinhasFinancasAPI.Entities
{
    public class Balance
    {
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("saldo")]
        public decimal Saldo { get; set; }



    }
}
