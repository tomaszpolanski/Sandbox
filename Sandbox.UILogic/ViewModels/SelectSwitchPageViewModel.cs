using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Sandbox.UILogic.Model;
using Utilities.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class SelectSwitchPageViewModel : ViewModel, IDisposable
    {
        public IList<City> CitiesList { get; private set; }
        public ReadonlyReactiveProperty<string> Population { get; private set; }
        public ReactiveCommand<IList<object>> ItemSelectedCommand { get; private set;}
        public ReactiveProperty<bool> IsMultipleSelection { get; private set; }

        public SelectSwitchPageViewModel()
        {
            CitiesList = Enumerable.Range(0, 5)
                                   .Select(number => new City { Population = (uint)number * 1000})
                                   .ToList();

            ItemSelectedCommand = new ReactiveCommand<IList<object>>();
            IsMultipleSelection = new ReactiveProperty<bool>(false);

            Population = IsMultipleSelection.Select(isMultipleSelection => isMultipleSelection ?
                                                     DefineMultipleSliderPopulationObservable(ItemSelectedCommand) :
                                                     DefineSingleSliderPopulationObservable(ItemSelectedCommand))
                                            .Switch()
                                            .ToReadonlyReactiveProperty();

        }

        public void Dispose()
        {
            ItemSelectedCommand.Dispose();
            Population.Dispose();
            IsMultipleSelection.Dispose();
        }

        private static IObservable<string> DefineSingleSliderPopulationObservable(IObservable<IList<object>> itemsSelectedObservable)
        {
            return itemsSelectedObservable.Select(selectedItems => selectedItems.OfType<City>()
                                                                                .FirstOrDefault())
                                          .Select(city => city != null ? Observable.FromEventPattern<long>(h => city.PopulationChanged += h,
                                                                                                           h => city.PopulationChanged -= h)
                                                                                   .Select(args => args.EventArgs)
                                                                                   .StartWith(city.Population)
                                                                       : Observable.Return<long>(0))
                                          .Switch()
                                          .StartWith(0)
                                          .Select(population => population.ToString());
        }

        private static IObservable<string> DefineMultipleSliderPopulationObservable(IObservable<IList<object>> itemsSelectedObservable)
        {
            return itemsSelectedObservable.Select(selectedItems => selectedItems.OfType<City>()
                                                                                .ToList())
                                          .Select(selectedItems => selectedItems.Any() ? selectedItems.ToObservable()
                                                                                                      .SelectMany(city => Observable.FromEventPattern<long>(h => city.PopulationChanged += h,
                                                                                                                                                            h => city.PopulationChanged -= h)
                                                                                                                                    .Select(args => city)
                                                                                                                                    .StartWith(city))
                                                                                                      .BucketSum(city => city.Population)
                                                                                       : Observable.Return<long>(0))
                                          .Switch()
                                          .StartWith(0)
                                          .Select(population => population.ToString());
        }
    }
}
