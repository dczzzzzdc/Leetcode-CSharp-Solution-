using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Linq;

namespace Array
{
    class PrefixSum
    {
        private readonly int[] nums;
        private readonly int[] prefix;

        public PrefixSum(int[] nums)
        {
            this.nums = nums;

            prefix = new int[nums.Length + 1];
            prefix[0] = 1;
            for (int i = 1; i <= nums.Length; i++)
            {
                prefix[i] = prefix[i - 1] + this.nums[i-1];
            }
        } 
        public int Query(int i, int j)
        {
            return prefix[j+1] - prefix[i];
        }
    }

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

        #region Leetcode 674  Longest Continuous Increasing Subsequence
        public int FindLengthOfLCIS(int[] nums)
        {
            if (nums.Length == 0 || nums == null)
            {
                return 0;
            }
            int max = 1;
            int len = 1;
            for (int i = 1; i < nums.Length; ++i)
            {
                if (nums[i] > nums[i - 1])
                {
                    len++;
                }
                else
                {
                    max = Math.Max(len, max);
                    len = 1;
                }
            }
            return Math.Max(max, len);
        }
        #endregion
        #region Leetcode 57  Insert Interval
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            int n = intervals.Length;
            List<int[]> result = new List<int[]>();
            if (n == 0)
            {
                result.Add(newInterval);
                return result.ToArray();
            }
            int index = 0;
            int newStart = newInterval[0];
            int newEnd = newInterval[1];
            int mergedEnd = newEnd; int mergedStart = newStart;
            while (index < n && intervals[index][1] < newStart)
            {
                result.Add(intervals[index++]);
            }
            while (index < n && intervals[index][0] <= newEnd)
            {
                mergedEnd = Math.Max(mergedEnd, intervals[index][1]);
                mergedStart = Math.Min(mergedStart, intervals[index][0]);
                ++index;
            }
            result.Add(new int[2] { mergedStart, mergedEnd });
            while (index < n)
            {
                result.Add(intervals[index++]);
            }
            return result.ToArray();
        }
        #endregion
        #region Leetcode 229  Majority Element II
        public IList<int> MajorityElement(int[] nums)
        {
            int len = nums.Length;
            if(len == 0)
            {
                return new List<int>();
            }
            int target = len / 3;

            int count1 = 0, count2 =0;
            int candidate1 = 0, candidate2 = 1;
            
            foreach(int n in nums)
            {
                if(n == candidate1)
                {
                    count1++;
                }
                else if(n == candidate2)
                {
                    count2++;
                }
                else if(count1 == 0)
                {
                    candidate1 = n;
                    count1 = 1;
                }
                else if(count2 == 0)
                {
                    candidate2 = n;
                    count2 = 1;
                }
                else
                {
                    count1--;
                    count2--;
                }
            }
            List<int> ans = new List<int>();
            int real1 = 0, real2 = 0;
            foreach(int n in nums)
            {
                if(n == candidate1)
                {
                    real1++;
                }
                else if(n == candidate2)
                {
                    real2++;
                }
            }
            if (real1 > target)
            {
                ans.Add(candidate1);
            }
            if (real2 > target)
            {
                ans.Add(candidate2);
            }
            return ans;
        }

        #endregion
        #region Leetcode 134  Gas Station
        public int CanCompleteCircuitBruteForce(int[] gas, int[] cost)
        {
            int n = gas.Length;
            for (int i = 0; i < n; ++i) // Try every starting point
            {
                int total = 0, count = 0, j = i;
                while (count < n)
                {
                    total += gas[j % n] - cost[j % n];
                    if (total < 0)
                    {
                        break;
                    }
                    ++j;
                    ++count;
                }
                if (count == n && total >= 0)
                {
                    return i;
                }
            }
            return -1;
        }

        // Since every problem is guaranteed to have a solution, whenever the tank is negative
        // the ith station and all its previous ones cannot be the answer
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int total = 0, tank = 0, ans = 0;

            for (int i = 0; i < cost.Length; i++)
            {
                int consume = gas[i] - cost[i];
                tank += consume;
                total += consume;

                if(tank < 0)
                {
                    tank = 0;
                    ans = i + 1;
                }
            }
            return total < 0 ? -1 : ans;
        }
        #endregion
        #region Leetcode 41  First Missing Positive
        // Number x should be on nums[x-1] 
        // The missing number must be in range from[1 - n] so we can just ignore the number that
        public int FirstMissingPositive(int[] nums)
        {
            int n = nums.Length;
            int i = 0;
            while(i < n)
            {
                if(nums[i] > 0 && nums[i] <= n && nums[i] != nums[nums[i]-1])
                {
                    int temp = nums[nums[i] - 1];
                    nums[nums[i] - 1] = nums[i];
                    nums[i] = temp;
                }
                else
                {
                    ++i;
                }
            }
            for (int x = 0; x < n; x++)
            {
                if(nums[x] != x + 1)
                {
                    return x + 1;
                }
            }
            return n + 1;
        }
        #endregion
    }
}
