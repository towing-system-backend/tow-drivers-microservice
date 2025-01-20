using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverEmail : IValueObject<TowDriverEmail>
    {
        private readonly string _value;
        
        public TowDriverEmail(string value)
        {
            if (!EmailRegex.IsEmail(value))
            {
                throw new InvalidTowDriverEmailException();
            }
            _value = value;
        }

        public string GetValue() => _value;
        public bool Equals(TowDriverEmail other) => _value == other._value;
    }
}
