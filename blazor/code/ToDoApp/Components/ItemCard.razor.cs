using Microsoft.AspNetCore.Components;

namespace ToDoApp;

public partial class ItemCard
{
    private bool isEditEnabled;

    [Parameter] public ToDoItem? Value { get; set; }

    [Parameter] public EventCallback<ToDoItem> ValueChanged { get; set; }

    protected override void OnParametersSet()
    {
        if (Value is null)
            throw new ArgumentNullException(nameof(Value));

        name = Value.Name;
        content = Value.Content;
    }

    private async Task StateChanged(ToDoState state)
    {
        if (Value is null)
            throw new ArgumentNullException(nameof(Value));

        Value = Value with
        {
            Name = name,
            Content = content,
            State = state,
            Archived = state == ToDoState.Finished
                ? DateTime.UtcNow
                : null
        };

        await ValueChanged.InvokeAsync(Value);

        if (isEditEnabled)
            isEditEnabled = false;
        else
            ToggleCollapse();
    }

    private void EnableEdit()
    {
        isEditEnabled = true;
        StateHasChanged();
        Focus();
    }

    private void CancelEdit()
    {
        name = Value?.Name ?? string.Empty;
        content = Value?.Content ?? string.Empty;
        isEditEnabled = false;
        collapsed = true;
    }
}