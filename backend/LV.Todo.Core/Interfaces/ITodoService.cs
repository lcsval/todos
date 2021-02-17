using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LV.Todo.Core.Models;

namespace LV.Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Task<TodoModel> CreateTodoAsync(TodoModel todoModel);
        Task<TodoModel> UpdateTodoAsync(TodoModel todoModel);
        Task<TodoModel> GetTodoAsync(TodoModel todoModel);
        Task DeleteTodoAsync(Guid todoId);
        Task<List<TodoModel>> GetTodosAsync();
    }
}