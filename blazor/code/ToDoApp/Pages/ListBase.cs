using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp {
    public abstract class ListBase : ComponentBase {
        protected bool shouldReload;
        protected List<ToDoItem> items = new();

        [Inject] protected LocalStorageService Storage { get; set; }

        protected override void OnInitialized() {
            shouldReload = true;
            Storage.KeyDataChanged += (s, e) => HandleStorageChanged(e);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (shouldReload) {
                items = await Storage.GetAsync<List<ToDoItem>>(Consts.ItemsStorageKey) ?? new();
                shouldReload = false;
                StateHasChanged();
            }
        }

        protected async Task ItemChanged(ToDoItem item) {
            this.items.RemoveAll(x => x.Id == item.Id);
            this.items.Add(item);
            await Storage.SetAsync(Consts.ItemsStorageKey, items);
        }

        protected void HandleStorageChanged(string key) {
            if (key != Consts.ItemsStorageKey)
                return;
            shouldReload = true;
        }
    }
}