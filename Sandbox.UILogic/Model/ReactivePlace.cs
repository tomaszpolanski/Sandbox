using System;
using System.Reactive.Subjects;

namespace Sandbox.UILogic.Model
{
    public class ReactivePlace
    {
        private readonly Subject<string> _addressSubject = new Subject<string>();
        private readonly Subject<bool> _isFavoriteSubject = new Subject<bool>();
        private readonly Subject<string> _nameSubject = new Subject<string>();
        private string _address;
        private bool _isFavorite;
        private string _name;

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    _addressSubject.OnNext(_address);
                }
            }
        }

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    _isFavoriteSubject.OnNext(_isFavorite);
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    _nameSubject.OnNext(_name);
                }
            }
        }

        public IObservable<string> AddressObservable
        {
            get { return _addressSubject; }
        }

        public IObservable<string> NameObservable
        {
            get { return _nameSubject; }
        }

        public IObservable<bool> IsFavoriteObservable
        {
            get { return _isFavoriteSubject; }
        }
    }
}