using Microsoft.AspNetCore.Components;

namespace ToDoApp;

public abstract class ItemCardBase : ComponentBase
{
    protected const string titleSelector = ".todoitem__input";
    protected bool focus;
    protected bool collapsed = true;
    protected string name = string.Empty;
    protected string content = string.Empty;

    protected bool IsEven => RowNumber % 2 == 0;

    [Parameter] public int RowNumber { get; set; }

    [Inject] public InteropService? Interop { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (focus)
        {
            if (Interop is null)
                throw new ArgumentNullException(nameof(Interop));

            await Interop.FocusAsync(titleSelector);
            focus = false;
        }
    }

    protected virtual void ToggleCollapse() => collapsed = !collapsed;
    protected virtual void Focus() => focus = true;

    protected virtual void HandleContent(ChangeEventArgs args)
        => content = (string?)args.Value ?? string.Empty;
}