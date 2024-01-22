using Newtonsoft.Json;

namespace AwsTodoApp.Models.Requests
{
    public class UpdateRecordRequest
    {
        [JsonProperty("itemName", Required = Required.Default)]
        public string? ItemName { get; set; }
        [JsonProperty("itemType", Required = Required.Default)]
        public string? ItemType { get; set; }
        [JsonProperty("importance", Required = Required.Default)]
        public int? Importance { get; set; }
    }
}
