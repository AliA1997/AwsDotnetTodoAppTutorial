using Newtonsoft.Json;

namespace AwsTodoApp.Models.Responses
{
    public class ReadRecordsResponse
    {
        [JsonProperty("todo-list", Required = Required.Always)]
        public List<TodoItemView> TodoList { get; set; }
    }
}
