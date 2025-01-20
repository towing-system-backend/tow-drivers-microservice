using Application.Core;
using System.Text.RegularExpressions;

namespace TowDriver.Domain
{
    public class TowDriverTowAssigned : IValueObject<TowDriverTowAssigned>
    {
        private readonly string _value;

        public TowDriverTowAssigned(string value) 
        {
            if (value.Equals("Not Assigned.") || !GuidEx.IsGuid(value))
            {
                _value = value;
            }

            if (GuidEx.IsGuid(value))
            {
                _value = value;
            }
        }

        public string GetValue() => _value;
        public bool Equals(TowDriverTowAssigned other) => _value == other._value;
    }
}
