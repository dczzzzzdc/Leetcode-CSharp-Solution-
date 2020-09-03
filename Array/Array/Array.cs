using System;
using System.Collections.Generic;
using System.Linq;

namespace Array
{
    class Array
    {
        static void Main(string[] args)
        {
            
        }
        #region Contain Duplicate Series  Leetcode 217/219/220
        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> seen = new HashSet<int>();
            foreach (int n in nums)
            {
                if (seen.Contains(n))
                {
                    return true;
                }
                seen.Add(n);
            }
            return false;
        }
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            HashSet<int> window = new HashSet<int>();
            // Keep a sliding window size of k
            // In other words, the last element in the Hashset should be i - k
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i > k)
                { 
                    window.Remove(nums[i - k - 1]);
                }

                if (window.Contains(nums[i]))
                {
                    return true;
                }
                window.Add(nums[i]);
            }
            return false;
        }
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            // Have t buckets of size k+1 (We actually need k+1 to hold two elements that the have the abs difference of k)
            // In this way, if there are two elements in the same bucket, then return true
            if (k < 1 || t < 0)
            {
                return false;
            }
            Dictionary<long, long> bucket = new Dictionary<long, long>();
            for (int i = 0; i < nums.Length; i++)
            {
                long value = (long)nums[i] - int.MinValue;
                // Since there are negative integers, a simple nums[i] / (t+1) will shrink everything towards zero
                // Therefore, we have to reposition every element to start from int.MinValue
                long index = value / (long)(t + 1);
                if (bucket.ContainsKey(index))
                {
                    return true;
                }
                if (bucket.ContainsKey(index - 1))
                {
                    if (Math.Abs(value - bucket[index - 1]) <= t)
                    {
                        return true;
                    }
                }
                if (bucket.ContainsKey(index + 1))
                {
                    if (Math.Abs(value - bucket[index + 1]) <= t)
                    {
                        return true;
                    }
                }
                if (bucket.Count >= k)
                {
                    long last_basket_index = ((long)nums[i - k] - int.MinValue) / (long)(t + 1);
                    bucket.Remove(last_basket_index);
                    // Removing the bucket of nums[i-k]
                }
                bucket[index] = value;
                // Put the value in the bucket
            }
            return false;
        }
        #endregion

        #region Ugly Number Series  Leetcode 263/264/313/1201
        // Definition of a ugly number: Positive numbers whose prime factors only includes 2,3 and 5

        // Check if the number is a ugly number
        public bool IsUgly(int num)
        {
            if(num <= 0)
            {
                return false;
            }
            for (int i = 2; i <= 5 && num > 0; i++)
            {
                while(num % i == 0)
                {
                    num /= i;
                }
            }
            return num == 1;
        }

        // Find the nth ugly number
        
        // Core theory, the sequence of ugly number is (1,2,3,4,5....) * all ugly factors
        // Therefore, on every stop, we just have to find the smallest one
        public int NthUglyNumber(int n)
        {
            int i2 = 0, i3 = 0, i5 = 0;
            int[] ugly = new int[n];
            ugly[0] = 1;
            for (int i = 1; i < n; i++)
            {
                int cur = Math.Min(Math.Min(2 * ugly[i2], 3 * ugly[i3]), 5 * ugly[i5]);
                if (ugly[i2] * 2 == cur) { ++i2; }
                if (ugly[i3] * 3 == cur) { ++i3; }
                if (ugly[i5] * 5 == cur) { ++i5; }
                ugly[i] = cur;
            }
            return ugly[n - 1];
        }

        public int NthSuperUglyNumber(int n, int[] primes)
        {
            int[] ugly = new int[n];
            int[] index = new int[primes.Length];
            ugly[0] = 1;
            for (int i = 1; i < n; ++i)
            {
                ugly[i] = int.MaxValue;
                for (int j = 0; j < primes.Length; ++j)
                {
                    ugly[i] = Math.Min(ugly[i], primes[j] * ugly[index[j]]);
                }
                for (int k = 0; k < primes.Length; ++k)
                {
                    if (ugly[i] >= primes[k] * ugly[index[k]])
                    {
                        ++index[k];
                    }
                }
            }
            return ugly[n - 1];
        }

        // Ugly Number III is a Binary Search problem

        public int NthUglyNumber(int n, int a, int b, int c)
        {
            int l = 1;
            int r = 2 * (int)Math.Pow(10, 9);
            while (l < r)
            {
                int mid = l + (r - l) / 2;
                if (CountMultiples(mid, a, b, c) >= n)
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }
            return l;
        }
        /// <summary>
        /// Count the multiple of a,b and c from 1 to n
        /// </summary>
        public int CountMultiples(long n, long a, long b, long c)
        {
            return (int)(n / a + n / b + n / c // The area of three circles
                - n / LeastCommonMultiple(a, b) - n / LeastCommonMultiple(a, c) - n / LeastCommonMultiple(b, c) // The overlapping area formed by two circles
                + n / (LeastCommonMultiple(LeastCommonMultiple(a, b), c))); // The overlapping area formed by all three circles
        }
        public long LeastCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }
        public long GreatestCommonDivisor(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a | b;
        }
        #endregion

    }
}
