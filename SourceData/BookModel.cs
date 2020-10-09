using Newtonsoft.Json;

namespace SourceData
{
    internal class BookModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("released")]
        public string Released { get; set; }
    }
}
