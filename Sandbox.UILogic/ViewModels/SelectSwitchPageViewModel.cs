using System;
using System.Collections;
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
            CitiesList = Enumerable.Range(0, 5)
                                   .Select(number => new City { Population = (uint)number * 1000 })
                                   .ToList();

            ItemSelectedCommand = new ReactiveCommand<IList<object>>();
            Population = ItemSelectedCommand.Select(selectedItems => selectedItems.FirstOrDefault())
                                            .Select(selectedItem => selectedItem as City)
                                            .Select(city => Observable.FromEventPattern<uint>(h => city.PopulationChanged += h,
                                                                                              h => city.PopulationChanged -= h)
                                                                      .Select(args => args.EventArgs)
                                                                      .StartWith(city.Population))
                                            .Switch()
                                            .Select(population => population.ToString())
                                            .ToReadonlyReactiveProperty();
        }

        public void Dispose()
        {
            Population.Dispose();
        }
    }
}
