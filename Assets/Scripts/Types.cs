using System;
using System.Collections;
using System.Collections.Generic;

namespace Types
{
    public enum DisplayAspectRatio
    {
        Auto,
        MatchWindow,
        Custom,
        R4_3,
        R16_9,
        R19_9,
        R20_9,
        PAR1_1,
        Count
    };

    public class TypesClass
    {
        private static readonly sbyte INT8_MIN = 0x7f;
        private static readonly short INT16_MAX = 0x7fff;
        private static readonly int INT32_MAX = 0x7fffffff;
        private static readonly long INT64_MAX = 0x7fffffffffffffff;

        public static int INT8_C(int x) { return x; }
        public static int INT16_C(int x) { return x; }
        public static int INT32_C(int x) { return (x + (INT32_MAX - INT32_MAX)); }
        public static int INT64_C(int x) { return (x + (int)(INT64_MAX - INT64_MAX)); }

        // Generic sign extension
        public static int SignExtend32(int value, int NBITS)
        {
            // http://graphics.stanford.edu/~seander/bithacks.html#VariableSignExtend
            int shift = 8 * sizeof(int) - NBITS;
            return (value << shift) >> shift;
        }

        public static long SignExtend64(long value, int NBITS)
        {
            // http://graphics.stanford.edu/~seander/bithacks.html#VariableSignExtend
            int shift = 8 * sizeof(long) - NBITS;
            return (value << shift) >> shift;
        }
    }
}