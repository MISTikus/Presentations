using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ToDoApp.Components {
    public abstract class ItemCardBase : ComponentBase {
        protected bool focus;
        protected bool collapsed = true;
        protected bool isEven => RowNumber % 2 > 0;
        protected string name = string.Empty;
        protected string content = string.Empty;

        [Parameter] public int RowNumber { get; set; }

        [Inject] public IJSRuntime Js { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (this.focus) {
                await Js.InvokeVoidAsync("focusElement", ".todoitem__input");
                this.focus = false;
            }
        }

        protected virtual void ToggleCollapse() => this.collapsed = !this.collapsed;

        protected virtual void Focus() => this.focus = true;

        protected virtual void HandleContent(ChangeEventArgs args) => this.content = (string) args.Value;
    }
}