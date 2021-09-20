using System;

namespace ToDoApp {
    public record ToDoItem(
        Guid Id,
        DateTime Created,
        string Name,
        string Content,
        ToDoState State = ToDoState.Created,
        DateTime? Archived = null);
}