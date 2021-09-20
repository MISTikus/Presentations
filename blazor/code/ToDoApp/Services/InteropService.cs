using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ToDoApp {
    public class InteropService {
        private readonly IJSRuntime js;

        public InteropService(IJSRuntime js) {
            this.js = js;
        }

        public async Task FocusAsync(string selector)
            => await this.js.InvokeVoidAsync("focusElement", selector);
    }
}