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

        public SelectSwitchPageViewModel()
        {
            CitiesList = Enumerable.Repeat(0, 5)
                                   .Select(number => new City { Population = (uint)number})
                                   .ToList();

            ItemSelectedCommand = new ReactiveCommand<IList<object>>();
            Population = ItemSelectedCommand.Select(selectedItems => selectedItems.FirstOrDefault())
                                            .OfType<City>()
                                            .Select(city => Observable.FromEventPattern<long>(h => city.PopulationChanged += h,
                                                                                              h => city.PopulationChanged -= h)
                                                                      .Select(args => args.EventArgs)
                                                                      .StartWith(city.Population))
                                            .Switch()
                                            .Select(population => population.ToString())
                                            .ToReadonlyReactiveProperty();

            Population = ItemSelectedCommand.Select(selectedItems => selectedItems.OfType<City>()
                                                                                  .ToList())
                                            .Select(selectedItems => selectedItems.ToObservable()
                                                                                  .SelectMany(city => Observable.FromEventPattern<long>(h => city.PopulationChanged += h,
                                                                                                                                        h => city.PopulationChanged -= h)
                                                                                                                .Select(args => city)
                                                                                                                .StartWith(city))
                                                                                  .BucketSum(city => city.Population))
                                            .Switch()
                                            .Select(population => population.ToString())
                                            .ToReadonlyReactiveProperty();

        }

        public void Dispose()
        {
            ItemSelectedCommand.Dispose();
            Population.Dispose();
        }
    }
}
