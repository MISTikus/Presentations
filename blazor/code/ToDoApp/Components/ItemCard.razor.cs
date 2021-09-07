using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Components {
    public partial class ItemCard : ComponentBase {
        private bool isEditEnabled;
        private bool focus;
        private bool collapsed = true;
        private bool isEven => RowNumber % 2 > 0;
        private string name = string.Empty;
        private string content = string.Empty;

        [Parameter] public int RowNumber { get; set; }

        [Parameter] public ToDoItem Value { get; set; }

        [Parameter] public EventCallback<ToDoItem> ValueChanged { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (this.focus) {
                await js.InvokeVoidAsync("focusElement", "#name");
                this.focus = false;
            }
        }

        protected override void OnParametersSet() {
            if (Value is null)
                throw new ArgumentNullException(nameof(Value));

            name = Value.Name;
            content = Value.Content;
        }

        private void ToggleCollapse() => this.collapsed = !this.collapsed;

        private async Task StateChanged(ToDoState state) {
            Value = Value with { Name = name, Content = content };

            if (state == ToDoState.Finished)
                Value = Value with { State = state, Archived = DateTime.UtcNow };
            else
                Value = Value with { State = state, Archived = null };

            await ValueChanged.InvokeAsync(Value);

            if (this.isEditEnabled)
                this.isEditEnabled = false;
            else
                ToggleCollapse();
        }

        private IEnumerable<DropDownItem> GetDropItems() {
            var stateItem = new DropDownItem(Value.State
                switch {
                    ToDoState.Created => "Завершить",
                        ToDoState.Finished => "Вернуть",
                        _ =>
                        throw new NotImplementedException()
                }, asyncClickAction : async() =>
                await StateChanged(Value.State == ToDoState.Created ? ToDoState.Finished : ToDoState.Created));

            var editItem = new DropDownItem("Изменить", () => {
                this.isEditEnabled = true;
                StateHasChanged();
            });

            return new DropDownItem[] { editItem, new DropDownItem(isDivider: true), stateItem };
        }

        private void HandleContent(ChangeEventArgs args) => this.content = (string) args.Value;
    }
}