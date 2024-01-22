using AwsTodoApp.Models;
using AwsTodoApp.Models.Requests;
using AwsTodoApp.Models.Responses;
using AwsTodoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwsTodoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ListController : ControllerBase
{
    private readonly ILogger<ListController> Logger;
    private readonly ISupabaseService SupabaseService;

    public ListController(
        ILogger<ListController> logger, 
        ISupabaseService supabaseService)
    {
        Logger = logger;
        SupabaseService = supabaseService;
    }

    [HttpGet]
    public async Task<ReadRecordsResponse> ReadTodoList()
    {
        //Get todo list by invoking the ReadRecordsAsync method and use a generic type of your DAO class.
        var todoList = await SupabaseService.ReadRecordsAsync();
        //return the todo list in a form of a read records response
        return new ReadRecordsResponse()
        {
            TodoList = todoList
        };
    }
    
    [HttpPost]
    public async Task<CreateRecordResponse> AddTodoItem([FromBody] CreateRecordRequest request)
    {
        //Create a new todo item
        var newlyCreatedTodoItemId = await SupabaseService.CreateRecordAsync(request);
        //return the todo list in a form of a read records response
        return new CreateRecordResponse()
        {
            Id = newlyCreatedTodoItemId
        };
    }


    //Add two methods for deleting and updating records, both taking id's in the request.
    [HttpPut("{id}")]
    public async Task<UpdateRecordResponse> UpdateTodoItem(int id, [FromBody] UpdateRecordRequest request)
    {
        //Create a new todo item
        await SupabaseService.UpdateRecordAsync(id, request);
        //return the todo list in a form of a read records response
        return new UpdateRecordResponse()
        {
            Id = id.ToString(),
            Success = true
        };
    }


    [HttpDelete("{id}")]
    public async Task<DeleteRecordResponse> DeleteTodoItem(int id)
    {
        //Create a new todo item
        await SupabaseService.DeleteRecordAsync(id);
        //return the todo list in a form of a read records response
        return new DeleteRecordResponse()
        {
            Success = true
        };
    }
}
