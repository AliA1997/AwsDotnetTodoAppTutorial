using Newtonsoft.Json;

namespace AwsTodoApp.Models.Requests
{
    public class CreateRecordRequest
    {
        [JsonProperty("itemName", Required = Required.Always)]
        public string? ItemName { get; set; }
        [JsonProperty("itemType", Required = Required.Always)]
        public string? ItemType { get; set; }
        [JsonProperty("importance", Required = Required.Always)]
        public int? Importance { get; set; }
    }
}