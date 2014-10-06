using System;

namespace Sandbox.UILogic.Model
{
    public class Place
    {
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
                    AddressChanged(this, _address);
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
                    IsFavoriteChanged(this, _isFavorite);
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
                    NameChanged(this, _name);
                }
            }
        }

        public event EventHandler<string> AddressChanged = delegate { };
        public event EventHandler<bool> IsFavoriteChanged = delegate { };
        public event EventHandler<string> NameChanged = delegate { };
    }
}