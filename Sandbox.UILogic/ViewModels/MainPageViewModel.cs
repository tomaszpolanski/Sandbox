using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;

namespace Sandbox.UILogic.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        public ICommand ShowReactivePropertiesCommand { get; private set; }
        public ICommand SubscriptionChangeCommand { get; private set; }
        public ICommand SandboxCommand { get; private set; }
        public ICommand PropertyUpdateCommand { get; private set; }
        public ICommand LatestWinsCommand { get; private set; }


        public MainPageViewModel(INavigationService navigationService)
        {
            ShowReactivePropertiesCommand =
                new DelegateCommand(() => navigationService.Navigate("ReactiveProperties", null));
            SubscriptionChangeCommand = new DelegateCommand(() => navigationService.Navigate("SubscriptionChange", null));
            SandboxCommand = new DelegateCommand(() => navigationService.Navigate("SandBox", null));
            PropertyUpdateCommand = new DelegateCommand(() => navigationService.Navigate("PropertyUpdate", null));
            LatestWinsCommand = new DelegateCommand(() => navigationService.Navigate("LatestWins", null));
        }
    }
}