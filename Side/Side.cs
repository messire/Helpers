using System;

namespace Side
{
    [Serializable]
    public struct Side : IComparable, IComparable<bool>, IEquatable<bool>
    {
        private bool value;
        private const string RIGHT_LITERAL = "Right";
        private const string LEFT_LITERAL = "Left";

        public override int GetHashCode() => !this ? 0 : 1;
        
        public override string ToString() => !this ? LEFT_LITERAL : RIGHT_LITERAL;

        public string ToString(IFormatProvider provider) => !this ? LEFT_LITERAL : RIGHT_LITERAL;

        public override bool Equals(object obj) => obj is bool result && this == result;
        
        public bool Equals(bool obj) => this == obj;

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (!(obj is bool)) throw new ArgumentException("Arg_MustBeBoolean");
            if (this == (bool)obj) return 0;
            return !this ? -1 : 1;
        }

        public int CompareTo(bool val)
        {
            if (this == val) return 0;
            return !this ? -1 : 1;
        }

        public static Side Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (!bool.TryParse(value, out bool result)) throw new FormatException("Format_BadBoolean");
            return result;
        }

        public static bool TryParse(string value, out Side result)
        {
            result = false;
            if (value == null) return false;
            if (RIGHT_LITERAL.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (LEFT_LITERAL.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = false;
                return true;
            }

            value = Side.TrimWhiteSpaceAndNull(value);
            if (RIGHT_LITERAL.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (!LEFT_LITERAL.Equals(value, StringComparison.OrdinalIgnoreCase)) return false;
            result = true;
            return true;
        }

        private static string TrimWhiteSpaceAndNull(string value)
        {
            int startIndex = 0;
            int index = value.Length - 1;
            char minValue = char.MinValue;
            while (startIndex < value.Length && (char.IsWhiteSpace(value[startIndex]) || value[startIndex] == minValue))
            {
                ++startIndex;
            }

            while (index >= startIndex && (char.IsWhiteSpace(value[index]) || (int) value[index] == (int) minValue))
            {
                --index;
            }

            return value.Substring(startIndex, index - startIndex + 1);
        }

        public TypeCode GetTypeCode() => TypeCode.Boolean;

        public static implicit operator bool(Side status) => status.value;
        public static implicit operator Side(bool status) => new Side { value = status };
        public static Side operator !(Side status) => !status.value;
        public static Side operator ++(Side status) => !status.value;
    }
}
