using System;

namespace ToDoApp.Models {
    public record ToDoItem(Guid Id, DateTime Created, string Name, string Content,
        ToDoState State = ToDoState.Created, DateTime? Archived = null);
}