using Newtonsoft.Json;

namespace AwsTodoApp.Models.Responses
{
    public class UpdateRecordResponse
    {
        [JsonProperty("id", Required = Required.Default)]
        public string? Id { get; set; }

        [JsonProperty("success", Required = Required.Default)]
        public bool? Success { get; set; }
    }
}
