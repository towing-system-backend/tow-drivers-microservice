using Application.Core;

namespace TowDriver.Domain
{
    public class TowDriverStatus : IValueObject<TowDriverStatus>
    {
        private readonly string _value;

        private static readonly string[] ValidStatuses = { "Active", "Inactive" };

        public TowDriverStatus(string value)
        {
            if (!IsValidStatus(value))
            {
                throw new InvalidTowDriverStatusException();
            }
            _value = value;
        }

        private static bool IsValidStatus(string value)
        {
            return Array.Exists(ValidStatuses, status => status.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        public string GetValue() => _value;
        public bool Equals(TowDriverStatus other) => _value == other._value;
    }
}
