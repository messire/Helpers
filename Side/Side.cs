using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Side
{
    [Serializable]
    public struct Side : IComparable, IComparable<bool>, IEquatable<bool>
    {
        private bool value;
        internal const bool Right = true;
        internal const bool Left = false;
        private const string RightLiteral = "Right";
        private const string LeftLiteral = "Left";

        public override int GetHashCode()
        {
            return !this ? 0 : 1;
        }

        public override string ToString()
        {
            return !this ? LeftLiteral : RightLiteral;
        }

        public string ToString(IFormatProvider provider)
        {
            return !this ? LeftLiteral : RightLiteral;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is bool)) return false;
            return this == (bool)obj;
        }

        public bool Equals(bool obj)
        {
            return this == obj;
        }

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
            bool result = false;
            if (!bool.TryParse(value, out result)) throw new FormatException("Format_BadBoolean");
            return result;
        }

        public static bool TryParse(string value, out Side result)
        {
            result = false;
            if (value == null) return false;
            if (RightLiteral.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (LeftLiteral.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = false;
                return true;
            }

            value = Side.TrimWhiteSpaceAndNull(value);
            if (RightLiteral.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }

            if (!LeftLiteral.Equals(value, StringComparison.OrdinalIgnoreCase)) return false;
            result = true;
            return true;
        }

        private static string TrimWhiteSpaceAndNull(string value)
        {
            int startIndex = 0;
            int index = value.Length - 1;
            char minValue = char.MinValue;
            while (startIndex < value.Length && (char.IsWhiteSpace(value[startIndex]) || (int)value[startIndex] == (int)minValue)) ++startIndex;
            while (index >= startIndex && (char.IsWhiteSpace(value[index]) || (int)value[index] == (int)minValue)) --index;
            return value.Substring(startIndex, index - startIndex + 1);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Boolean;
        }

        public static implicit operator bool(Side status) => status.value;
        public static implicit operator Side(bool status) => new Side { value = status };
        public static Side operator !(Side status) => !status.value;
        public static Side operator ++(Side status) => !status.value;

    }
}
