using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public static class ExtensionMethods
    {
        //thread-safe invoke//
        public static void SynchronizedInvoke(this ISynchronizeInvoke sync, Action action)
        {
            if (!sync.InvokeRequired)
            {
                action();
                return;
            }

            sync.Invoke(action, null);
        }

        //get random item from a list//
        public static T Random<T>(this IEnumerable<T> list, MT19937_64 rng) => list.ElementAt(rng.Next(list.Count()));

        //randomly shuffle the items in a list//
        public static List<T> Shuffle<T>(this IEnumerable<T> oldList, MT19937_64 rng)
        {
            var newList = new List<T>(oldList);
            var count = newList.Count;
            for (int i = 0; i < count - 1; i++)
            {
                int k = rng.Next(i, count);
                T temp = newList[i];
                newList[i] = newList[k];
                newList[k] = temp;
            }

            return newList;
        }

        public static byte[] ToByteArray(this string byteString)
        {
            if (byteString == null)
                return null;

            var list = new List<byte>();
            foreach (var subString in byteString.Split())
                list.Add(byte.Parse(subString, System.Globalization.NumberStyles.HexNumber));

            return list.ToArray();
        }

        public static int[] ToIntArray(this string intString)
        {
            if (intString == null)
                return null;

            var list = new List<int>();
            foreach (var subString in intString.Split())
                list.Add(byte.Parse(subString, System.Globalization.NumberStyles.HexNumber));

            return list.ToArray();
        }

        public static bool TryFirst<T>(this IEnumerable<T> list, out T result)
        {
            result = list.FirstOrDefault();
            if (result.Equals(default(T)))
                return false;

            return true;
        }

        public static int GetSetBits(int i)
        {
            i -= ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            i = (i + (i >> 4)) & 0x0F0F0F0F;
            return (i * 0x01010101) >> 24;
        }
    }
}
