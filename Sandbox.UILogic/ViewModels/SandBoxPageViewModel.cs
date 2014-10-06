using System.Reactive.Subjects;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace Sandbox.UILogic.ViewModels
{
    public class SandBoxPageViewModel : ViewModel
    {
        private readonly Subject<object> _buttonClick = new Subject<object>();

        public ICommand ClickCommand { get; private set; }

        public SandBoxPageViewModel()
        {
            ClickCommand = new DelegateCommand(() => _buttonClick.OnNext(null));
        }
    }
}