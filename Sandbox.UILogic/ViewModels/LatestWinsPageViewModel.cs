using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Utilities.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class LatestWinsPageViewModel : ViewModel, IDisposable
    {
        public ReadonlyReactiveProperty<string> Description { get; private set; }
        public ReactiveCommand CountPressesCommand { get; private set; }

        public LatestWinsPageViewModel()
        {
            CountPressesCommand = new ReactiveCommand();
            Description = CountPressesCommand.Select((_, count) => count)
                                             .Select(count => Observable.FromAsync(token => SlowCounterAsync(count, token)))
                                             .Switch()
                                             .ToReadonlyReactiveProperty();

        }

        public void Dispose()
        {
            CountPressesCommand.Dispose();
            Description.Dispose();
        }

        private async Task<string> SlowCounterAsync(int count, CancellationToken token)
        {
            await Task.Delay(500, token);
            token.ThrowIfCancellationRequested();
            return string.Format("This is the {0} time you have pressed the button", count);
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            Dispose();
        }
    }
}