using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace BIT
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Bitwise operator
             *  And & (Both)
             *  If both two bits are 1, the result is 1
             *  
             *  Or | (Either)
             *  If any of the bit is 1, then the result is 1
             *  
             *  Xor ^ (Different)
             *  If only one of the bits is 1, the result is 1
             * 
             *  Not ~ (Invert)
             *  Revert all the bits
             */
            byte a = 25;
            byte b = 7;
            Console.WriteLine("a: " + Convert.ToString(a, 2).PadLeft(8,'0'));
            Console.WriteLine("b: " + Convert.ToString(b, 2).PadLeft(8,'0'));
            Console.WriteLine("--------");
            Console.WriteLine("And Operation");
            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("Or Operation");
            Console.WriteLine(Convert.ToString(a|b,2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("Xor Operation");
            Console.WriteLine(Convert.ToString(a ^ b,2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("A Not Operation");
            Console.WriteLine(Convert.ToString((byte)~a,2));
            // maximum value = orginal value + reverted value
            Console.WriteLine();

            byte c = 45;
            Console.WriteLine(Convert.ToString(c,2).PadLeft(8,'0'));
            byte left_shift_one = (byte)(c << 1); // Moving every to the left by one tile
            Console.WriteLine("Left by one");
            Console.WriteLine(Convert.ToString(left_shift_one,2).PadLeft(8,'0'));
            byte right_shift_one = (byte)(c >> 1);
            Console.WriteLine("Right by one");
            Console.WriteLine(Convert.ToString(right_shift_one,2).PadLeft(8,'0'));
            // We lose the last digit
        }
        #region Leetcode 190  Reverse Bits
        public uint reverseBits(uint n)
        {
            uint ans = 0;
            for (int i = 0; i < 32; i++) // We must make leading zeros to ending zeros
            {
                ans = (ans << 1) | (n & 1);
                n >>= 1; // This deletes the last digit
            }
            return ans;
        }
        #endregion
        #region Leetcode 762  Prime Number of Set Bits in Binary Representation
        public int CountPrimeSetBits(int L, int R)
        {
            int ans = 0;
            int mask = 665772;
            // 1010 0010 1000 1010 1100
            for (int i = L; i <= R; i++)
            {
                if ((mask & (1 << CountBits(i))) !=0) { ++ans; }
            }
            return ans;
        }
        private int CountBits(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count += (n & 1);
                n >>= 1;
            }
            return count;
        }
        #endregion
        #region Leetcode 136  Single Number
        public int SingleNumber(int[] nums)
        {
            /*Property of XOR
             * N^N = 0
             * N^0 = N
             * Therefor if we XOR ans with every element, the two same number will be counted as zero
             * Only the single number will be left
             */
            int ans = 0;
            foreach (int num in nums)
            {
                ans ^= num;
            }
            return ans;
        }
        #endregion
        #region Leetcode 137  Single Number II 
        public int SingleNumberII(int[] nums)
        {
            int res = 0;
            for (int i = 0; i < 32; i++)
            {
                int sum = 0; 
                // Count of the amount of ones
                for (int j = 0; j < nums.Length; j++)
                {
                    if(((nums[j]>> i)& 1 )== 1)
                    {
                        ++sum;
                    }
                }
                sum %= 3;
                // Since all other number occured three times, if the bit[i] of the single number is 1, the sum must be 3n+1
                if(sum == 1)
                {
                    res += sum << i;
                    // Push 1 to the ith bit
                }
            }
            return res;
        }
        #endregion
        #region Leetcode 268  Missing Number 
        public int MissingNumber(int[] nums)
        {
            // nums.Length definitely replace one of the number from 0 ~ nums.Length -1
            // We have to initiallize the missing number as nums.Length in order to offset the actual value
            int missing = nums.Length;
            for (int i = 0; i < nums.Length; i++)
            {
                missing ^= i ^ nums[i];
            }
            return missing;
            /*Theory: 
             * N^N = 0 
             * Supposedly, nums[i] = i
             * However, when there is a missing number, nums[i-1] = i
             * Finally, nums[nums.Length-1] = nums.Length
             */
            /*Proof Walkthrough
             * Number 2 is missing here
             * Index	0	1	2	3
             * Value	0	1	3	4
             * 4 ^ (0^0) ^ (1^1）^(2^3) ^ (3^4)
             * 0 ^ 0 ^ 2^ (3^3) ^ (4^4)
             * The result is 2
             */
        }
        #endregion
    }
}
