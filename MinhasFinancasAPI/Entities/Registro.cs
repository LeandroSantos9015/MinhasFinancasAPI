using MinhasFinancasAPI.Util.Enumerator;
using System.Text.Json.Serialization;

namespace MinhasFinancasAPI.Entities
{
    public class Registro
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("value")]
        public decimal Valor { get; set; }

        [JsonPropertyName("type")]
        public string Tipo { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }




    }

    
}
