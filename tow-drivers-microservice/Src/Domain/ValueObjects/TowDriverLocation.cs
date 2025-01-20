using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverLocation : IValueObject<TowDriverLocation>
    {
        private readonly string _value;

        public TowDriverLocation(string value)
        {
            if (value == null)
            {
                throw new InvalidTowDriverLocationException();
            }
            _value = value;
        }

        public string GetValue() => _value;
        public bool Equals(TowDriverLocation other) => _value == other._value;
    }
}
