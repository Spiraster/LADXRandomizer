namespace LADXRandomizer
{
    public class MT19937
    {
        private const int n = 624;

        private uint[] mt = new uint[n];
        private int index;

        public MT19937(uint seed)
        {
            index = n;
            mt[0] = seed;

            for (uint i = 1; i < n; i++)
                mt[i] = 1812433253 * (mt[i - 1] ^ (mt[i - 1] >> 30)) + i;
        }

        public uint Next()
        {
            if (index >= n)
                Twist();

            uint y = mt[index++];
            y ^= y >> 1;
            y ^= (y << 7) & 0x9d2c5680;
            y ^= (y << 15) & 0xefc60000;
            y ^= y >> 18;

            return y;
        }

        public int Next(int max)
        {
            return (int)(Next() * (1.0 / uint.MaxValue) * max);
        }

        public int Next(int min, int max)
        {
            int range = max - min;
            return (int)(Next() * (1.0 / uint.MaxValue) * range) + min;

        }

        private void Twist()
        {
            for (int i = 0; i < n; i++)
            {
                uint x = (mt[i] & 0x80000000) + (mt[(i + 1) % n] & 0x7fffffff);
                uint xA = x >> 1;

                if (x % 2 != 0)
                    xA ^= 0x9908b0df;

                mt[i] = mt[(1 + 397) % n] ^ xA;
            }

            index = 0;
        }
    }

    public static class MurmurHash3
	{
		const uint c1 = 0xcc9e2d51;
		const uint c2 = 0x1b873593;

        private static uint Mix (uint k, uint h)
        {
            k *= c1;
            k = (k << 15) | (k >> 17);
            k *= c2;

            return h ^= k;
        }
        
        public static uint Hash(byte[] data, uint seed = 0xdeadbeef)
		{
			int initialLength = data.Length;
            int currentLength = initialLength;

			uint h = seed;
            uint k;

            int currentIndex = 0;
			while (currentLength >= 4)
			{
				k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex++] << 16 | data[currentIndex++] << 24);

                h = Mix(k, h);
                h = (h << 13) | (h >> 19);
                h = h * 5 + 0xe6546b64;

				currentLength -= 4;
			}

			switch (currentLength)
			{
				case 3:
					k = (uint)(data[currentIndex++] | data[currentIndex++] << 8 | data[currentIndex] << 16);
                    h = Mix(k, h);
                    break;
				case 2:
					k = (uint)(data[currentIndex++] | data[currentIndex] << 8);
                    h = Mix(k, h);
                    break;
				case 1:
					k = data[currentIndex];
                    h = Mix(k, h);
                    break;
			}

            h ^= (uint)initialLength;
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

			return h;
		}
	}
}