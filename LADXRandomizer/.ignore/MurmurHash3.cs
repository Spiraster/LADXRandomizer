namespace LADXRandomizer
{
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