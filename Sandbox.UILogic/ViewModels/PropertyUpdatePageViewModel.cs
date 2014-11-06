using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Microsoft.Practices.Prism.Mvvm;
using Sandbox.UILogic.Model;
using Utilities.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class PropertyUpdatePageViewModel : ViewModel, IDisposable
    {
        private readonly Place _place = new Place {Name = "Home", Address = "Lezajsk", IsFavorite = true};

        public string DisplayText
        {
            get
            {
                return string.Format("This place name is: {0}, the address {1} and it is favorite: {2}", _place.Name,
                    _place.Address, _place.IsFavorite);
            }
        }

        public ReadonlyReactiveProperty<string> ReactiveDisplayText { get; private set; }

        #region Helpers

        public string PlaceName
        {
            get { return _place.Name; }
            set { _place.Name = value; }
        }

        public string PlaceAddress
        {
            get { return _place.Address; }
            set { _place.Address = value; }
        }

        public bool PlaceIsFavorite
        {
            get { return _place.IsFavorite; }
            set { _place.IsFavorite = value; }
        }

        #endregion

        public PropertyUpdatePageViewModel()
        {
            _place.NameChanged += OnNameChanged;
            _place.AddressChanged += OnAddressChanged;
            _place.IsFavoriteChanged += OnIsFavoriteChanged;

            #region Reactive stuff

            var nameObservable = Observable.FromEventPattern<string>(h => _place.NameChanged += h,
                h => _place.NameChanged -= h)
                .Select(args => args.EventArgs)
                .StartWith(_place.Name);

            var addressObservable = Observable.FromEventPattern<string>(h => _place.AddressChanged += h,
                h => _place.AddressChanged -= h)
                .Select(args => args.EventArgs)
                .StartWith(_place.Address);

            var isFavoriteObservable = Observable.FromEventPattern<bool>(h => _place.IsFavoriteChanged += h,
                h => _place.IsFavoriteChanged -= h)
                .Select(args => args.EventArgs)
                .StartWith(_place.IsFavorite);

            ReactiveDisplayText =
                nameObservable.CombineLatest(addressObservable, isFavoriteObservable, (name, address, isFavorite) =>
                    string.Format("This place name is: {0}, the address {1} and it is favorite: {2}", name,
                        address, isFavorite))
                    .ToReadonlyReactiveProperty();

            #endregion
        }

        public void Dispose()
        {
            ReactiveDisplayText.Dispose();
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            Dispose();
        }

        private void OnIsFavoriteChanged(object sender, bool e)
        {
            OnPropertyChanged(() => DisplayText);
        }

        private void OnAddressChanged(object sender, string e)
        {
            OnPropertyChanged(() => DisplayText);
        }

        private void OnNameChanged(object sender, string e)
        {
            OnPropertyChanged(() => DisplayText);
        }
    }
}