using Postgrest.Attributes;
using Postgrest.Models;

namespace AwsTodoApp.Models
{
    [Table("todo-items")]
    public class TodoItem: BaseModel
    {
        [PrimaryKey("id")]
        public long Id { get; set; }

        [Column("item_name")]
        public string ItemName { get; set; }

        [Column("item_type")]
        public string ItemType { get; set; }

        [Column("importance")]
        public string Importance { get; set; }
    }
}
