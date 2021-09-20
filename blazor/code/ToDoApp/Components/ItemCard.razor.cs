using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ToDoApp {
    public partial class ItemCard {
        private bool isEditEnabled;

        [Parameter] public ToDoItem Value { get; set; }

        [Parameter] public EventCallback<ToDoItem> ValueChanged { get; set; }

        protected override void OnParametersSet() {
            if (Value is null)
                throw new ArgumentNullException(nameof(Value));

            name = Value.Name;
            content = Value.Content;
        }

        private async Task StateChanged(ToDoState state) {
            Value = Value with {
                Name = name,
                Content = content,
                State = state,
                Archived = state == ToDoState.Finished
                    ? DateTime.UtcNow
                    : null
                };

            await ValueChanged.InvokeAsync(Value);

            if (this.isEditEnabled)
                this.isEditEnabled = false;
            else
                ToggleCollapse();
        }

        private void EnableEdit() {
            this.isEditEnabled = true;
            StateHasChanged();
            Focus();
        }

        private void CancelEdit() {
            this.name = Value.Name;
            this.content = Value.Content;
            this.isEditEnabled = false;
            this.collapsed = true;
        }
    }
}