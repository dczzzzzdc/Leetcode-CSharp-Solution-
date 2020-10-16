﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.Pipes;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Xml.Schema;

namespace BIT
{
    
    public class PrefixXOR
    {
        public int[] prefix;

        public PrefixXOR(int[] n)
        {
            prefix = new int[n.Length + 1];
            
            for(int i = 1; i <= n.Length; ++i)
            {
                prefix[i] = prefix[i - 1] ^ n[i - 1];
            }
        }
        /// <summary>
        /// Query XOR sum from arr[i] to arr[j]
        /// </summary>
        /// <param name="i">The starting index(inclusive)</param>
        /// <param name="j">The ending index(inclusive)</param>
        public int Query(int i, int j)
        {
            return prefix[j + 1] ^ prefix[i];
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    class SortByBitCountComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            int a = PopCount(x);
            int b = PopCount(y);
            if (a == b)
            {
                return x < y ? -1 : 1;
                // The number that has smaller value should be put at the front
            }
            else
            {
                return a < b ? -1 : 1;
            }
        }
        private int PopCount(int n)
        {
            int count = 0;
            while (n != 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return count;
        }
    }
    class BIT
    {
        static void Main(string[] args)
        {
            #region Basic Bitwise Operator
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
            Console.WriteLine("a: " + Convert.ToString(a, 2).PadLeft(8, '0'));
            Console.WriteLine("b: " + Convert.ToString(b, 2).PadLeft(8, '0'));
            Console.WriteLine("--------");
            Console.WriteLine("And Operation");
            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Or Operation");
            Console.WriteLine(Convert.ToString(a | b, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Xor Operation");
            Console.WriteLine(Convert.ToString(a ^ b, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("A Not Operation");
            Console.WriteLine(Convert.ToString((byte)~a, 2));
            // maximum value = orginal value + reverted value
            Console.WriteLine();
            #endregion
            #region Bit Shifting
            byte c = 45;
            Console.WriteLine(Convert.ToString(c, 2).PadLeft(8, '0'));
            byte left_shift_one = (byte)(c << 1); // Moving every bit to the left by one tile
            Console.WriteLine("Left by one");
            Console.WriteLine(Convert.ToString(left_shift_one, 2).PadLeft(8, '0'));
            byte right_shift_one = (byte)(c >> 1);
            Console.WriteLine("Right by one");
            Console.WriteLine(Convert.ToString(right_shift_one, 2).PadLeft(8, '0'));
            // We lose the last digit
            Console.WriteLine();
            #endregion
            #region Operations
            Console.WriteLine("Set Union");
            Console.WriteLine(Convert.ToString(a | b, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Set Subtraction");
            Console.WriteLine(Convert.ToString(a & ~b, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Get all 1 bit");
            Console.WriteLine(Convert.ToString(~0, 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Remove the last bit");
            Console.WriteLine(Convert.ToString(a & (a - 1), 2).PadLeft(8, '0'));
            Console.WriteLine();
            Console.WriteLine("Extract the last bit");
            Console.WriteLine(Convert.ToString(a & (-a), 2));
            Console.WriteLine();
            Console.WriteLine("Set Bit");
            int n = a;
            int val = 1;
            n = n << 1 | val; // Put a 1 at the end of the number
            Console.WriteLine(Convert.ToString(n, 2).PadLeft(8, '0'));
            #endregion
        }

        #region Extension: Count set bits
        public int CountSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                if ((n & 1) > 0)
                {
                    ++count;
                }
                n >>= 1;
            }
            return count;
        }
        #endregion
        #region Extension: Convert to Bases
        public string BaseNeg2(int N)
        {
            StringBuilder result = new StringBuilder();

            while (N != 0)
            {
                result.Insert(0, N & 1);
                N = -(N >> 1);
            }

            return result.Length == 0 ? "0" : result.ToString();
        }
        public string Base10To2(int n)
        {
            StringBuilder res = new StringBuilder();

            while (n != 0)
            {
                res.Append(n & 1);
                n >>= 1;
            }
            return res.Length > 0 ? res.ToString() : "0";
        }
        public int Base2To10(string s)
        {
            int result = 0;
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                result += (int)Char.GetNumericValue(s[i]) * (int)Math.Pow(2, n - i - 1);
            }
            return result;
        }
        public string Base10Tok(int n, int k)
        {
            StringBuilder res = new StringBuilder();
            while (n != 0)
            {
                res.Insert(0, n % k);
                n /= k;
            }
            return res.ToString();
        }
        #endregion
        #region Check ith Bit
        public bool Ithsetbit(int n, int i)
        {
            return (n & (1 << i)) == 0;
        }
        #endregion
        #region Extension: Gray Code
        public int GrayCode(int n)
        {
            // Gray Code is binary numeric system where two successive values differ in only one bit
            // When the ith bit of n is 1, the ith bit of G(n) will be 1 and the (i+1)th bit will be 0, and vice versa

            return n ^ (n >> 1);
        }
        #endregion
        #region Leetcode 1310  XOR Queries of a Subarray
        public int[] XorQueries(int[] arr, int[][] queries)
        {
            PrefixXOR p = new PrefixXOR(arr);
            int[] ans = new int[queries.Length];
            int i = 0;
            foreach (int[] q in queries)
            {
                ans[i++] = p.Query(q[0], q[1]);
            }
            return ans;
        }
        #endregion
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
            // The prime number bits can only be 2,3,5,7,11,13,17,19 
            // because R is smaller that 10 ^ 6 and at most have 20 bits
            for (int i = L; i <= R; i++)
            {
                if ((mask & (1 << CountBits(i))) != 0) { ++ans; }
            }
            return ans;
        }
        public int CountBits(int n)
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
                    if (((nums[j] >> i) & 1) == 1)
                    {
                        ++sum;
                    }
                }
                sum %= 3;
                // Since all other number occured three times, if the bit[i] of the single number is 1, the sum must be 3n+1
                if (sum == 1)
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
        #region Leetcode 191  Number of 1 Bits
        public int HammingWeight(uint n)
        {
            int count = 0;
            for (int i = 0; i < 32; i++)
            {
                count += (int)(n & 1);
                n >>= 1;
            }
            return count;
        }
        #endregion
        #region Leetcode 343  Power of Four
        public bool IsPowerOfFour(int num)
        {
            return (num > 0 && ((num & (num - 1)) == 0) && ((num & 0x55555555) != 0));
            /* Explanation of ((num & (num - 1)) == 0)
             * This checks whether this number is the power of 2
             * For example, 2 => 0010, 4=> 0100
             * The common point is that they all ends with a zero
             */
            /* Explanation of (num & 0x55555555) != 0)
             * 0x55555555 => 0b1010101010101010101010101010101
             * 4 => 0100, 16 => 0001 0000
             * The common point is that the one is always on the odd postion(3,5)
             * Therefore, after anding with 0x55555555, which only has 1 on odd digits
             * The power of 4 should give 1
             */
        }
        #endregion
        #region Leetcode 389  Find the difference
        public char FindTheDifference(string s, string t)
        {
            int n = s.Length;
            char ans = t[n];
            for (int i = 0; i < n; i++)
            {
                ans ^= s[i];
                ans ^= t[i];
            }
            return ans;
        }
        #endregion
        #region Leetcode 461  Hamming Distance
        public int HammingDistance(int x, int y)
        {
            int length = 0;
            int xor = x ^ y;
            while (xor > 0)
            {
                length += xor & 1;
                xor >>= 1;
            }
            return length;
        }
        #endregion
        #region Leetcod 476  Number Complement
        public int FindComplement(int num)
        {
            // We first invert the number and then XOR it with a Binary Mask to delete its leading zero
            int mask = ~0;
            // Set the mask to 1111 1111
            while ((mask & num) != 0)
            // This will delete the last 1 and make it 0
            { mask <<= 1; }
            // Eventually, we will get the right amount of 1 in the mask to offset the leading zeros
            return mask ^ ~num;
        }
        #endregion
        #region Leetcode 371  Sum of two integars
        public int GetSum(int a, int b)
        {
            if (a == 0) { return b; }
            else if (b == 0) { return a; }
            while (b != 0) // Iterate until there is no carry
            {
                int carry = a & b; // Carry Simulation
                a ^= b; // Addition simulation
                b = carry << 1; // We store the carry to use it next time
            }
            return a;
        }
        #endregion
        #region Leetcode 1290  Convert Binary Number in a Linked List to Integer
        public int GetDecimalValue(ListNode head)
        {
            int ans = 0;
            while (head != null)
            {
                ans = ans << 1 | head.val;
                // ans = a * 2 + head.val;
                head = head.next;
            }
            return ans;
        }
        #endregion
        #region Leetcode 1356  Sort Integers by The Number of 1 Bits
        public int[] SortByBits(int[] arr)
        {
            Array.Sort(arr, new SortByBitCountComparer());
            return arr;
        }
        #endregion
        #region Leetcode 784  Letter Case Permutation
        public IList<string> LetterCasePermutation(string S)
        {
            LCPdfs(0, new StringBuilder(""), S);
            return LCPans;
        }
        IList<string> LCPans = new List<string>();
        public void LCPdfs(int index, StringBuilder cur, string word)
        {
            if (index == word.Length)
            {
                LCPans.Add(cur.ToString());
            }
            cur.Append(word[index]);
            if (Char.IsNumber(word[index]))
            {
                LCPdfs(index + 1, cur, word);
            }
            else
            {
                LCPdfs(index + 1, cur, word);
                cur.Remove(cur.Length - 1, 1);
                if (Char.IsUpper(word[index]))
                {
                    cur.Append(Char.ToLower(word[index]));
                }
                else
                {
                    cur.Append(Char.ToUpper(word[index]));
                }
                LCPdfs(index + 1, cur, word);
            }
            cur.Remove(index, 1);
        }
        #endregion
        #region Leetcode 421  Maximum XOR of Two Numbers in an Array
        public int FindMaximumXOR(int[] nums)
        {
            // If a ^ b = max then a ^ max = b
            int max = 0;
            int mask = 0;
            for (int i = 30; i >= 0; --i) // We try to build the maximum number from the highest bit
            {
                int one = 1 << i;
                mask |= one;
                HashSet<int> set = new HashSet<int>();
                foreach (int n in nums)
                {
                    set.Add(n & mask);
                }
                foreach (int value in set)
                {
                    if (set.Contains(value ^ (max | one)))
                    {
                        max |= one; // Adding an one on the current bit
                        break;
                    }
                }
            }
            return max;
        }
        #endregion
        #region Leetcode 1238  Circular Permutation in Binary Representation
        public IList<int> CircularPermutation(int n, int start)
        {

            IList<int> ans = new List<int>();
            for (int i = 0; i < 1 << n; ++i)
            {
                ans.Add(start ^ i ^ i >> 1);
            }
            return ans;
        }
        #endregion
        #region Leetcode 898  Bitwise ORs of Subarrays
        public int SubarrayBitwiseORs(int[] A)
        {
            HashSet<int> res = new HashSet<int>(), prev = new HashSet<int>(), cur;

            foreach (int n in A)
            {
                cur = new HashSet<int>();
                cur.Add(n);

                foreach (int i in prev)
                {
                    cur.Add(n | i);
                }

                prev = cur;

                foreach (int p in prev)
                {
                    res.Add(p);
                }

            }

            return res.Count;
        }
        #endregion
        #region Leetcode 1552  Count Triplets That Can Form Two Arrays of Equal XOR
        public int CountTriplets(int[] arr)
        {
            PrefixXOR p = new PrefixXOR(arr);
            // for a = b, a ^ b = 0. 
            // Therefore, arr[i] ^ arr[i+1]  ^...^ arr[k]
            // Once we settle a legal range of (i,k), we have k - i choice because i < j <= k
            int n = arr.Length;
            int count = 0;
            for (int i = 0; i < n; ++i)
            {
                for (int j = i + 1; j < n; ++j)
                {
                    if (p.Query(i, j) == 0)
                    {
                        count += j - i;
                    }
                }
            }
            return count;
        }
        #endregion
        #region Leetcode 756  Pyramid Transition Matrix
        public bool PyramidTransition(string bottom, IList<string> allowed)
        {
            Dictionary<string, List<char>> a = new Dictionary<string, List<char>>();
            for (int i = 0; i < allowed.Count; ++i)
            {
                string cur = allowed[i];
                string key = cur.Substring(0, 2);
                if (!a.ContainsKey(key))
                {
                    a[key] = new List<char>();
                }
                a[key].Add(cur[2]);
            }
            return PT_dfs(bottom, a);
        }
        private bool PT_dfs(string cur, Dictionary<string, List<char>> a)
        {
            int n = cur.Length;
            if (n == 1)
            {
                return true;
            }
            for (int i = 0; i < n - 1; ++i)
            {
                if (!a.ContainsKey(cur.Substring(i, 2)))
                {
                    return false;
                }
            }
            List<string> possible = new List<string>();
            GetList(ref possible, a, cur, 0, new StringBuilder());
            foreach (string s in possible)
            {
                if (PT_dfs(s, a))
                {
                    return true;
                }
            }
            return false;
        }
        private void GetList(ref List<string> possible, Dictionary<string, List<char>> a, string cur, int index, StringBuilder sb)
        {
            if (index == cur.Length - 1)
            {
                possible.Add(sb.ToString());
                return;
            }
            foreach (char c in a[cur.Substring(index, 2)])
            {
                sb.Append(c);
                GetList(ref possible, a, cur, index + 1, sb);
                sb.Remove(sb.Length - 1, 1);
            }
        }
        #endregion

    }
}
