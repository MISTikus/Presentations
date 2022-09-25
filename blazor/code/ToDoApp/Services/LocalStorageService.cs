using Microsoft.JSInterop;
using System.Text.Json;

namespace ToDoApp;

public class LocalStorageService
{
    private const string getFunctionName = "localStorage.getItem";
    private const string setFunctionName = "localStorage.setItem";

    private readonly IJSRuntime js;

    public event EventHandler<string>? KeyDataChanged;

    public LocalStorageService(IJSRuntime js) => this.js = js;

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await js.InvokeAsync<string>(getFunctionName, key);
        return json is null
            ? default
            : JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        await js.InvokeVoidAsync(setFunctionName, key, JsonSerializer.Serialize(value));
        KeyDataChanged?.Invoke(this, key);
    }
}