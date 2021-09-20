using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ToDoApp {
    public abstract class ItemCardBase : ComponentBase {
        protected const string titleSelector = ".todoitem__input";
        protected bool focus;
        protected bool collapsed = true;
        protected bool isEven => RowNumber % 2 == 0;
        protected string name = string.Empty;
        protected string content = string.Empty;

        [Parameter] public int RowNumber { get; set; }

        [Inject] public InteropService Interop {get;set;}

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (this.focus) {
                await Interop.FocusAsync(titleSelector);
                this.focus = false;
            }
        }

        protected virtual void ToggleCollapse() => this.collapsed = !this.collapsed;
        protected virtual void Focus() => this.focus = true;

        protected virtual void HandleContent(ChangeEventArgs args) 
            => this.content = (string) args.Value;
    }
}