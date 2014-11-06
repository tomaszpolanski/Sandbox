using Microsoft.Practices.Prism.Mvvm;
using Utilities.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class SandBoxPageViewModel : ViewModel
    {
        public ReactiveProperty<string> Text { get; private set; }
        public ReactiveCommand ClickCommand { get; private set; }

        public SandBoxPageViewModel()
        {
            ClickCommand = new ReactiveCommand();
            Text = new ReactiveProperty<string>();
        }
    }
}