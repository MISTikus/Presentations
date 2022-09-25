using Microsoft.AspNetCore.Components;

namespace ToDoApp;

public abstract class ListBase : ComponentBase
{
    protected bool shouldReload;
    protected List<ToDoItem> items = new();

    [Inject] protected LocalStorageService? Storage { get; set; }

    protected override void OnInitialized()
    {
        shouldReload = true;

        if (Storage is null)
            throw new ArgumentNullException(nameof(Storage));

        Storage.KeyDataChanged += (s, e) => HandleStorageChanged(e);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (shouldReload)
        {
            if (Storage is null)
                throw new ArgumentNullException(nameof(Storage));

            items = await Storage.GetAsync<List<ToDoItem>>(Consts.ItemsStorageKey) ?? new();
            shouldReload = false;
            StateHasChanged();
        }
    }

    protected async Task ItemChanged(ToDoItem item)
    {
        if (Storage is null)
            throw new ArgumentNullException(nameof(Storage));

        var index = items.FindIndex(x => x.Id == item.Id);
        items[index] = item;
        await Storage.SetAsync(Consts.ItemsStorageKey, items);
    }

    protected void HandleStorageChanged(string key)
    {
        if (key != Consts.ItemsStorageKey)
            return;
        shouldReload = true;
    }
}