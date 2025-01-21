using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public static class Base62
    {
        private const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase62(ulong input)
        {
            var output = "";

            if (input == 0)
                return output += alphabet[0];

            ulong i = input;
            while (i > 0)
            {
                int digit = (int)(i % 62);
                output = alphabet[digit] + output;
                i /= 62;
            }

            return output;
        }

        public static ulong Parse(string input) => FromBase62(input);

        public static bool TryParse(string input, out ulong output)
        {
            output = 0;

            if (input.ToList().Exists(x => !alphabet.Contains(x)))
                return false;

            output = FromBase62(input);
            return true;
        }

        private static ulong FromBase62(string input)
        {
            ulong output = 0;
            ulong factor = 1;

            foreach (var digit in input.Reverse())
            {
                output += factor * (uint)alphabet.IndexOf(digit);
                factor *= 62;
            }

            return output;
        }
    }
}
