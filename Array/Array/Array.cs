using System;
using System.Collections.Generic;

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

    }
}
