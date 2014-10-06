using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Sandbox.UILogic.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class ReactivePropertiesPageViewModel : ViewModel, IDisposable
    {
        public ReadonlyReactiveProperty<string> RetriesText { get; private set; }


        public ReactivePropertiesPageViewModel()
        {
            RetriesText = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(number => "This is " + number + " retry")
                .ToReadonlyReactiveProperty();
        }

        public void Dispose()
        {
            RetriesText.Dispose();
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            Dispose();
        }
    }
}