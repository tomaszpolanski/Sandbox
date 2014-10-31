using System;

namespace Sandbox.UILogic.Model
{
    public class City : Place
    {
        private uint _population;

        public uint Population
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

        public event EventHandler<uint> PopulationChanged = delegate { };
    }
}
