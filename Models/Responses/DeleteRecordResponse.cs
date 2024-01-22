using Newtonsoft.Json;

namespace AwsTodoApp.Models.Responses
{
    public class DeleteRecordResponse
    {

        [JsonProperty("success", Required = Required.Default)]
        public bool? Success { get; set; }
    }
}
