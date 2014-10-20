using System.Reactive.Subjects;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Threading.Tasks;
using System.Threading;
using Utilities.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;
using System;

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