using System;

namespace Sandbox.UILogic.Model
{
    public class City : Place
    {
        private long _population;

        public long Population
        {
            get { return _population; }
            set
            {
                if (_population != value)
                {
                    _population = value;
                    PopulationChanged(this, _population);
                }
            }
        }

        public event EventHandler<long> PopulationChanged = delegate { };
    }
}
