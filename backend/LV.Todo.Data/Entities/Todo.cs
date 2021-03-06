using System;

namespace LV.Todo.Data.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}