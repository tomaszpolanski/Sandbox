using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Utilities.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class LatestWinsPageViewModel : ViewModel, IDisposable
    {
        private readonly ReactiveProperty<int> _pressCount = new ReactiveProperty<int>(0,
            ReactivePropertyMode.DistinctUntilChanged);

        public ReadonlyReactiveProperty<string> Description { get; private set; }

        public ICommand CountPressesCommand { get; private set; }

        public LatestWinsPageViewModel()
        {
            CountPressesCommand = new DelegateCommand(() => _pressCount.Value++);
            Description = _pressCount.Select(count => Observable.FromAsync(token => SlowCounterAsync(count, token)))
                .Switch()
                .ToReadonlyReactiveProperty();

            #region Reactive stuff

            //Description =
            //    _pressCount.Select(count => Observable.FromAsync(token => SlowCounterAsync(count, token)))
            //        .Switch()
            //        .ToReadonlyReactiveProperty();

            #endregion
        }

        public void Dispose()
        {
            _pressCount.Dispose();
            Description.Dispose();
        }

        private async Task<string> SlowCounterAsync(int count, CancellationToken token)
        {
            await Task.Delay(2000, token);
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