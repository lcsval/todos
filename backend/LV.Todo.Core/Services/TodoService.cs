using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LV.Todo.Core.Interfaces;
using LV.Todo.Core.Models;
using LV.Todo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LV.Todo.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoModel> CreateTodoAsync(TodoModel todoModel)
        {
            if (todoModel is null) 
            {
                throw new ArgumentNullException(nameof(todoModel));
            }

            var todoEntity = new Data.Entities.Todo {
                Description = todoModel.Description,
                IsCompleted = todoModel.IsCompleted,
                CreatedAt = DateTime.Now
            };

            todoEntity = await _todoRepository.AddAsync(todoEntity);

            return new TodoModel {
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
                CreatedAt = todoEntity.CreatedAt,
                UpdatedAt = todoEntity.UpdatedAt,
                CompletedAt = todoEntity.CompletedAt
            };
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            await _todoRepository.RemoveAsync(todoId);
        }

        public async Task<TodoModel> GetTodoAsync(TodoModel todoModel)
        {
            var todoEntity = await _todoRepository.FindAsync(todoModel.Id);

            if (todoEntity is null) {
                return null;
            }

            return new TodoModel {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
                CreatedAt = todoEntity.CreatedAt,
                UpdatedAt = todoEntity.UpdatedAt,
                CompletedAt = todoEntity.CompletedAt
            };
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            IQueryable<Data.Entities.Todo> query = _todoRepository.Get();
            return await query.Select(todo => new TodoModel {
                Id = todo.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt,
                UpdatedAt = todo.UpdatedAt,
                CompletedAt = todo.CompletedAt
            })
            .ToListAsync();
        }

        public async Task<TodoModel> UpdateTodoAsync(TodoModel todoModel)
        {
            var todoEntity = new Data.Entities.Todo {
                Id = todoModel.Id,
                Description = todoModel.Description,
                IsCompleted = todoModel.IsCompleted,
                CreatedAt = todoModel.CreatedAt,
                UpdatedAt = todoModel.UpdatedAt,
                CompletedAt = todoModel.CompletedAt
            };

            todoEntity = await _todoRepository.UpdateAsync(todoEntity);

            return new TodoModel {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
                CreatedAt = todoEntity.CreatedAt,
                UpdatedAt = todoEntity.UpdatedAt,
                CompletedAt = todoEntity.CompletedAt
            };
        }
    }
}