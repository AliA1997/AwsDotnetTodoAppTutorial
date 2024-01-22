using Newtonsoft.Json;

namespace AwsTodoApp.Models.Responses
{
    public class CreateRecordResponse
    {
        [JsonProperty("id", Required = Required.Always)]
        public string Id { get; set; }
    }
}
