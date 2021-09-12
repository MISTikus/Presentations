using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Components {
    public partial class ItemCard : ItemCardBase {
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

        private void EnableEdit() {
            this.isEditEnabled = true;            
            Focus();
            StateHasChanged();
        }
    }
}