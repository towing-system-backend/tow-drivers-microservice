using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverLocation : IValueObject<TowDriverLocation>
    {
        private readonly string _value;

        public TowDriverLocation(string value)
        {
            if ( value.Length < 8 || value.Length > 20)
            {
                throw new InvalidTowDriverLocationException();
            }
            _value = value;
        }
        public string GetValue() => _value;
        public bool Equals(TowDriverLocation other) => _value == other._value;
    }
}
