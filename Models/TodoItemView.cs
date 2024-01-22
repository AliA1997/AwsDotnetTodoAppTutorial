using Newtonsoft.Json;
using Postgrest.Attributes;

namespace AwsTodoApp.Models
{
    public class TodoItemView
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("itemName", Required = Required.Always)]
        public string ItemName { get; set; }

        [JsonProperty("itemType", Required = Required.Always)]
        public string ItemType { get; set; }

        [JsonProperty("importance", Required = Required.Always)]
        public string Importance { get; set; }
    }
}
