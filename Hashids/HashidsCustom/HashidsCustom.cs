﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HashidsCustom
{
    /// <summary>
    /// Generate YouTube-like hashes from one or many numbers. Use hashids when you do not want to expose your database ids to the user.
    /// </summary>
    public class Hashids
    {
        public const string DEFAULT_ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string DEFAULT_SEPS = "cfhistuCFHISTU";

        private const int MIN_ALPHABET_LENGTH = 16;
        private const double SEP_DIV = 3.5;
        private const double GUARD_DIV = 12.0;

        private string alphabet;
        private string salt;
        private string seps;
        private string guards;
        private int hashLength;

        private Regex guardsRegex;
        private Regex sepsRegex;

        //  Creates the Regex in the first usage, speed up first use of non hex methods
#if CORE
        private static Lazy<Regex> hexValidator = new Lazy<Regex>(() => new Regex("^[0-9a-fA-F]+$"));
        private static Lazy<Regex> hexSplitter = new Lazy<Regex>(() => new Regex(@"[\w\W]{1,12}"));
#else
        private static Lazy<Regex> hexValidator = new Lazy<Regex>(() => new Regex("^[0-9a-fA-F]+$", RegexOptions.Compiled));
        private static Lazy<Regex> hexSplitter = new Lazy<Regex>(() => new Regex(@"[\w\W]{1,12}", RegexOptions.Compiled));
#endif

        /// <summary>
        /// Instantiates a new Hashids with the default setup.
        /// </summary>
        public Hashids() : this(string.Empty, 0, DEFAULT_ALPHABET, DEFAULT_SEPS) { }

        /// <summary>
        /// Instantiates a new Hashids en/de-coder.
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="hashLength"></param>
        /// <param name="alphabet"></param>
        public Hashids(string salt = "", int hashLength = 0, string alphabet = DEFAULT_ALPHABET, string seps = DEFAULT_SEPS)
        {
            if (string.IsNullOrWhiteSpace(alphabet))
                throw new ArgumentNullException("alphabet");

            this.salt = salt;
            this.alphabet = new string(alphabet.ToCharArray().Distinct().ToArray());
            this.seps = seps;
            this.hashLength = hashLength;

            if (this.alphabet.Length < 16)
                throw new ArgumentException("alphabet must contain atleast 4 unique characters.", "alphabet");

            SetupSeps();
            SetupGuards();
        }

        /// <summary>
        /// Encodes the provided longs to a hashed string
        /// </summary>
        /// <param name="numbers">the numbers</param>
        /// <returns>the hashed string</returns>
        public string Encode(params ulong[] numbers) => EncodeInternal(numbers);

        /// <summary>
        /// Encodes the provided longs to a hashed string
        /// </summary>
        /// <param name="numbers">the numbers</param>
        /// <returns>the hashed string</returns>
        public string Encode(IEnumerable<ulong> numbers) => EncodeInternal(numbers.ToArray());

        /// <summary>
        /// Internal function that does the work of creating the hash
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private string EncodeInternal(ulong[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                return string.Empty;

            string ret = "";
            var alphabet = this.alphabet;

            ulong numbersHash = 0;
            for (var i = 0; i < numbers.Length; i++)
                numbersHash += numbers[i] % (uint)(i + 100);

            var lottery = alphabet[(int)(numbersHash % (uint)alphabet.Length)];
            ret += lottery;

            for (var i = 0; i < numbers.Length; i++)
            {
                var buffer = lottery + salt + alphabet;

                alphabet = ConsistentShuffle(alphabet, buffer.Substring(0, alphabet.Length));
                var last = Hash(numbers[i], alphabet);

                ret += last;

                if (i + 1 < numbers.Length)
                {
                    var sepsIndex = (int)((numbers[i] % (uint)(last[0] + i)) % (uint)seps.Length);
                    ret += seps[sepsIndex];
                }
            }

            if (ret.Length < hashLength)
            {
                var guardIndex = (int)((numbersHash + ret[0]) % (uint)guards.Length);
                ret = guards[guardIndex] + ret;

                if (ret.Length < hashLength)
                {
                    guardIndex = (int)((numbersHash + ret[2]) % (uint)guards.Length);
                    ret += guards[guardIndex];
                }
            }

            var halfLength = alphabet.Length / 2;
            while (ret.Length != hashLength)
            {
                alphabet = ConsistentShuffle(alphabet, alphabet);
                ret = alphabet.Substring(halfLength) + ret + alphabet.Substring(0, halfLength);

                var excess = ret.Length - hashLength;
                if (excess > 0)
                    ret = ret.Substring(excess / 2, hashLength);
            }

            return ret;
        }

        private string Hash(ulong input, string alphabet)
        {
            var hash = new StringBuilder();

            do
            {
                hash.Insert(0, alphabet[(int)(input % (uint)alphabet.Length)]);
                input = (input / (uint)alphabet.Length);
            } while (input > 0);

            return hash.ToString();
        }
        
        private string ConsistentShuffle(string alphabet, string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
                return alphabet;

            int n;
            var letters = alphabet.ToCharArray();
            for (int i = letters.Length - 1, v = 0, p = 0; i > 0; i--, v++)
            {
                v %= salt.Length;
                p += (n = salt[v]);
                var j = (n + v + p) % i;
                // swap characters at positions i and j
                var temp = letters[j];
                letters[j] = letters[i];
                letters[i] = temp;
            }

            return new string(letters);
        }
        
        private void SetupSeps()
        {
            // seps should contain only characters present in alphabet; 
            seps = new string(seps.ToCharArray().Intersect(alphabet.ToCharArray()).ToArray());

            // alphabet should not contain seps.
            alphabet = new string(alphabet.ToCharArray().Except(seps.ToCharArray()).ToArray());

            seps = ConsistentShuffle(seps, salt);

            if (seps.Length == 0 || (alphabet.Length / seps.Length) > SEP_DIV)
            {
                var sepsLength = (int)Math.Ceiling(alphabet.Length / SEP_DIV);
                if (sepsLength == 1)
                    sepsLength = 2;

                if (sepsLength > seps.Length)
                {
                    var diff = sepsLength - seps.Length;
                    seps += alphabet.Substring(0, diff);
                    alphabet = alphabet.Substring(diff);
                }

                else seps = seps.Substring(0, sepsLength);
            }

#if CORE
            sepsRegex = new Regex(string.Concat("[", seps, "]"));
#else
            sepsRegex = new Regex(string.Concat("[", seps, "]"), RegexOptions.Compiled);
#endif
            alphabet = ConsistentShuffle(alphabet, salt);
        }
        
        private void SetupGuards()
        {
            var guardCount = (int)Math.Ceiling(alphabet.Length / GUARD_DIV);

            if (alphabet.Length < 3)
            {
                guards = seps.Substring(0, guardCount);
                seps = seps.Substring(guardCount);
            }

            else
            {
                guards = alphabet.Substring(0, guardCount);
                alphabet = alphabet.Substring(guardCount);
            }

#if CORE
            guardsRegex = new Regex(string.Concat("[", guards, "]"));
#else
            guardsRegex = new Regex(string.Concat("[", guards, "]"), RegexOptions.Compiled);
#endif
        }

    }
}
