using Application.Core;

namespace TowDrivers.Domain
{
    public class TowDriverIdentificationNumber : IValueObject<TowDriverIdentificationNumber>
    {
        private readonly int _value;

        public TowDriverIdentificationNumber(int value)
        {
            if (value < 999999 || value > 40000000)
            {
                throw new InvalidTowDriverIdentificationNumberException();
            }
            _value = value;
        }
        public int GetValue() => _value;
        public bool Equals(TowDriverIdentificationNumber other) => _value == other._value;
    }
}
