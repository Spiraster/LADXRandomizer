namespace LADXRandomizer
{
    //public class MT19937
    //{
    //    const int w = 32;
    //    const int n = 624;
    //    const int m = 397;
    //    //const int r = 31;

    //    const uint a = 0x9908B0DF;

    //    const int u = 11;
    //    const uint d = 0xFFFFFFFF;

    //    const int s = 7;
    //    const uint b = 0x9D2C5680;

    //    const int t = 15;
    //    const uint c = 0xEFC60000;

    //    const int l = 18;

    //    const uint f = 0x6C078965;

    //    private uint[] mt = new uint[n];
    //    private int index;

    //    public MT19937(uint seed)
    //    {
    //        index = n;
    //        mt[0] = seed;

    //        for (uint i = 1; i < n; i++)
    //            mt[i] = f * (mt[i - 1] ^ (mt[i - 1] >> 30)) + i;
    //    }

    //    public uint Next()
    //    {
    //        if (index >= n)
    //            Twist();

    //        uint y = mt[index++];
    //        y ^= (y >> u) & d;
    //        y ^= (y << s) & b;
    //        y ^= (y << t) & c;
    //        y ^= y >> l;

    //        return y;
    //    }

    //    public int Next(int max)
    //    {
    //        return (int)(Next() * (1.0 / uint.MaxValue) * max);
    //    }

    //    public int Next(int min, int max)
    //    {
    //        int range = max - min;
    //        return (int)(Next() * (1.0 / uint.MaxValue) * range) + min;

    //    }

    //    private void Twist()
    //    {
    //        for (int i = 0; i < n; i++)
    //        {
    //            uint x = (mt[i] & 0x80000000) + (mt[(i + 1) % n] & 0x7fffffff);
    //            uint xA = x >> 1;

    //            if (x % 2 != 0)
    //                xA ^= a;

    //            mt[i] = mt[(i + m) % n] ^ xA;
    //        }

    //        index = 0;
    //    }
    //}

    public class MT19937_64
    {
        const int w = 64;
        const int n = 312;
        const int m = 156;
        //const int r = 31;

        const ulong a = 0xB5026F5AA96619E9;

        const int u = 29;
        const ulong d = 0x5555555555555555;

        const int s = 17;
        const ulong b = 0x71D67FFFEDA60000;

        const int t = 37;
        const ulong c = 0xFFF7EEE000000000;

        const int l = 43;

        const ulong f = 0x5851F42D4C957F2D;

        const ulong maxValue = ulong.MaxValue;

        private ulong[] mt = new ulong[n];
        private int index;

        public MT19937_64(ulong seed)
        {
            index = n;
            mt[0] = seed;

            for (ulong i = 1; i < n; i++)
                mt[i] = f * (mt[i - 1] ^ (mt[i - 1] >> 30)) + i;
        }

        public ulong Next()
        {
            if (index >= n)
                Twist();

            ulong y = mt[index++];
            y ^= (y >> u) & d;
            y ^= (y << s) & b;
            y ^= (y << t) & c;
            y ^= y >> l;

            if (y == maxValue)
                y--;

            return y;
        }

        public int Next(int max)
        {
            return (int)(Next() * (1.0 / maxValue) * max);
        }

        public int Next(int min, int max)
        {
            int range = max - min;
            return (int)(Next() * (1.0 / maxValue) * range) + min;

        }

        private void Twist()
        {
            for (int i = 0; i < n; i++)
            {
                ulong x = (mt[i] & 0x80000000) + (mt[(i + 1) % n] & 0x7fffffff);
                ulong xA = x >> 1;

                if (x % 2 != 0)
                    xA ^= a;

                mt[i] = mt[(i + m) % n] ^ xA;
            }

            index = 0;
        }
    }
}