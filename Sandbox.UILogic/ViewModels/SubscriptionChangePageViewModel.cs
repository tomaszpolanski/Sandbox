using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Sandbox.UILogic.Model;
using Sandbox.UILogic.Reactive;

namespace Sandbox.UILogic.ViewModels
{
    public class SubscriptionChangePageViewModel : ViewModel, IDisposable
    {
        public readonly List<Place> _places = new List<Place>
        {
            new Place {Name = "Home", Address = "Lezajsk", IsFavorite = true},
            new Place {Name = "Work", Address = "Berlin", IsFavorite = false},
            new Place {Name = "Retirement place", Address = "Hawaii", IsFavorite = true}
        };

        private string _currentAddress;
        private bool _currentFavorite;
        private int _currentIndex;
        private string _currentName;

        private Place _currentPlace;

        public string CurrentName
        {
            get { return _currentName; }
            private set
            {
                if (_currentName != value)
                {
                    _currentName = value;
                    OnPropertyChanged(() => CurrentName);
                }
            }
        }

        public string CurrentAddress
        {
            get { return _currentAddress; }
            private set
            {
                if (_currentAddress != value)
                {
                    _currentAddress = value;
                    OnPropertyChanged(() => CurrentAddress);
                }
            }
        }

        public bool CurrentFavorite
        {
            get { return _currentFavorite; }
            private set
            {
                if (_currentFavorite != value)
                {
                    _currentFavorite = value;
                    OnPropertyChanged(() => CurrentFavorite);
                }
            }
        }

        #region Reactive stuff

        private readonly ReactiveProperty<Place> _currentReactivePlace = new ReactiveProperty<Place>();

        public ReadonlyReactiveProperty<string> CurrentReactiveName { get; private set; }
        public ReadonlyReactiveProperty<string> CurrentReactiveAddress { get; private set; }
        public ReadonlyReactiveProperty<bool> CurrentReactiveFavorite { get; private set; }

        #endregion

        public ICommand NextCommand { get; private set; }

        public SubscriptionChangePageViewModel()
        {
            SetPlace(_places[_currentIndex]);
            NextCommand = new DelegateCommand(() => SetPlace(_places[++_currentIndex%_places.Count]));

            #region Reactive stuff

            CurrentReactiveName =
                _currentReactivePlace.Select(place => Observable.FromEventPattern<string>(h => place.NameChanged += h,
                    h => place.NameChanged -= h)
                    .Select(args => args.EventArgs)
                    .StartWith(place.Name))
                    .Switch()
                    .ToReadonlyReactiveProperty();

            CurrentReactiveAddress =
                _currentReactivePlace.Select(
                    place => Observable.FromEventPattern<string>(h => place.AddressChanged += h,
                        h => place.AddressChanged -= h)
                        .Select(args => args.EventArgs)
                        .StartWith(place.Address))
                    .Switch()
                    .ToReadonlyReactiveProperty();

            CurrentReactiveFavorite =
                _currentReactivePlace.Select(
                    place => Observable.FromEventPattern<bool>(h => place.IsFavoriteChanged += h,
                        h => place.IsFavoriteChanged -= h)
                        .Select(args => args.EventArgs)
                        .StartWith(place.IsFavorite))
                    .Switch()
                    .ToReadonlyReactiveProperty();

            #endregion
        }

        public void Dispose()
        {
            CurrentReactiveName.Dispose();
            CurrentReactiveAddress.Dispose();
            CurrentReactiveFavorite.Dispose();
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            Dispose();
        }

        private void SetPlace(Place place)
        {
            SetNormalPlace(place);

            _currentReactivePlace.Value = place;
        }

        private void SetNormalPlace(Place place)
        {
            if (_currentPlace != null)
            {
                _currentPlace.NameChanged -= OnNameChanged;
                _currentPlace.AddressChanged -= OnAddressChanged;
                _currentPlace.IsFavoriteChanged -= OnIsFavoriteChanged;
            }
            _currentPlace = place;
            CurrentName = _currentPlace.Name;
            CurrentAddress = _currentPlace.Address;
            CurrentFavorite = _currentPlace.IsFavorite;
            if (_currentPlace != null)
            {
                _currentPlace.NameChanged += OnNameChanged;
                _currentPlace.AddressChanged += OnAddressChanged;
                _currentPlace.IsFavoriteChanged += OnIsFavoriteChanged;
            }
        }

        private void OnIsFavoriteChanged(object sender, bool isFavorite)
        {
            CurrentFavorite = isFavorite;
        }

        private void OnAddressChanged(object sender, string address)
        {
            CurrentAddress = address;
        }

        private void OnNameChanged(object sender, string name)
        {
            CurrentName = name;
        }
    }
}