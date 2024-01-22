
using AwsTodoApp.Models;
using AwsTodoApp.Models.Requests;
using Newtonsoft.Json;
using Postgrest.Models;

namespace AwsTodoApp.Services
{
    public enum TodoItemImportance
    {
        NotImportant = 0,
        SomewhatImportant = 1,
        Important = 2,
        VeryImportant = 3
    }
    public interface ISupabaseService
    {
        public Task<List<TodoItemView>> ReadRecordsAsync();
        public Task<string> CreateRecordAsync(CreateRecordRequest request);
        public Task UpdateRecordAsync(int id, UpdateRecordRequest request);
        public Task DeleteRecordAsync(int id);
    }
    public class SupabaseService : ISupabaseService
    {
        //Inject supabase client, that would be responsible for getting data from our postgresql table.
        private readonly Supabase.Client SupabaseClient;
        public SupabaseService(Supabase.Client supabaseClient)
        {
            SupabaseClient = supabaseClient;
        }

        //Define two empty methods based on the ISupabase interface.
        public async Task<List<TodoItemView>> ReadRecordsAsync()
        {

            //Use supabase client, with a generic being your todo dao to get all records
            var recordsToReturn = await SupabaseClient.From<TodoItem>().Get();

            return recordsToReturn.Models.Select(itm => new TodoItemView() 
            { 
                Id = (int)itm.Id,
                ItemName = itm.ItemName,
                ItemType = itm.ItemType,
                Importance = itm.Importance
            }).ToList();
        }
        public async Task<string> CreateRecordAsync(CreateRecordRequest request)
        {
            //Instantiate new instance of the DAO which would be the TodoItem.
            var newTodoItem = new TodoItem()
            {
                ItemName = request.ItemName ?? "",
                ItemType = request.ItemType ?? "",
                Importance = GetImportanceOfTodoItem(request.Importance)
            };

            //Insert record in postgresql database
            var createdTodoItemResponse = await SupabaseClient.From<TodoItem>().Insert(newTodoItem);

            //Return newly created record id.
            return createdTodoItemResponse.Models.First().Id.ToString();
        }
        private string GetImportanceOfTodoItem(int? importance)
        {
            if (importance == (int)TodoItemImportance.SomewhatImportant) return "Somewhat Important";
            if (importance == (int)TodoItemImportance.Important) return "Important";
            if (importance == (int)TodoItemImportance.VeryImportant) return "Very Important";
            return "Not Important";
        }

        //Define update method that takes id, and a updaterecordrequest request.
        public async Task UpdateRecordAsync(int id, UpdateRecordRequest request)
        {
            var recordToUpdate = (await SupabaseClient.From<TodoItem>().Where(t => t.Id == id).Get()).Models.First();
            if (!string.IsNullOrWhiteSpace(request.ItemName)) recordToUpdate.ItemName = request.ItemName;
            if (!string.IsNullOrWhiteSpace(request.ItemType)) recordToUpdate.ItemType = request.ItemType;
            if (request.Importance != null) recordToUpdate.Importance = GetImportanceOfTodoItem(request.Importance);
            
            await SupabaseClient.From<TodoItem>()
                  .Upsert(recordToUpdate);

            return;
        }

        //Define delete method that takes id, and a updaterecordrequest request.
        public async Task DeleteRecordAsync(int id)
        {
            await SupabaseClient.From<TodoItem>()
                                .Where(t => t.Id == id)
                                .Delete();

            return;
        }
    }
}
