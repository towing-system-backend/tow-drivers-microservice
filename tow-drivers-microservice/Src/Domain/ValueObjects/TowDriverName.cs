using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverName : IValueObject<TowDriverName>
    {
        private readonly string _value;

        public TowDriverName(string value)
        {
            if (value.Length < 2 || value.Length > 64)
            {
                throw new InvalidTowDriverNameException();
            }
            _value = value;
        }

        public string GetValue() => _value;
        public bool Equals(TowDriverName other) => _value == other._value;
    }
}
