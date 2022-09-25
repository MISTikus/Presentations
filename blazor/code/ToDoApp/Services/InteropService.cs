using Microsoft.JSInterop;

namespace ToDoApp;

public class InteropService
{
    private readonly IJSRuntime js;

    public InteropService(IJSRuntime js) => this.js = js;

    public async Task FocusAsync(string selector)
        => await js.InvokeVoidAsync("focusElement", selector);
}