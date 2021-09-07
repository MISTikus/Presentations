using System;
using System.Threading.Tasks;

namespace ToDoApp.Models {
    public record DropDownItem {
        public bool IsDivider { get; }
        public string Caption { get; }
        public Action ClickAction { get; }
        public Func<Task> AsyncClickAction { get; }

        public DropDownItem(string caption, Action clickAction = null, Func<Task> asyncClickAction = null) {
            this.Caption = caption;
            this.ClickAction = clickAction;
            this.AsyncClickAction = asyncClickAction;
        }

        public DropDownItem(bool isDivider) {
            this.IsDivider = isDivider;
        }

    }
}