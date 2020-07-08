using System;
using System.Collections.Generic;
using System.Linq;

namespace DP_Array_Problems
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 698  Partition to K Equal Sum Subsets
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            int sum = nums.Sum();
            if (sum % k != 0) // If we cannot divide teh array equally
            {
                return false;
            }
            int subsum = sum / k;
            Array.Sort(nums);
            int index = nums.Length - 1;
            if (nums[index] > subsum)
            {
                return false;
            }
            while (index >= 0 && nums[index] == subsum)
            //If the current number is equal to the target sum, then it is a group itself
            // Therefore, we only need k-1 groups right now
            {
                index--;
                k--;
            }
            return partition(nums, index, new int[k], subsum);
        }
        public bool partition(int[] nums, int index, int[] subset, int target)
        // Subset is an array containing the sum of every groups
        {
            if (index < 0) // We successfully processed every number
            {
                return true;
            }
            int selected = nums[index];
            for (int i = 0; i < subset.Length; i++) // We can try out to put this current number in any existing group in subset
            {
                if (subset[i] + selected <= target)// If we can put this in
                {
                    subset[i] += selected;
                    if (partition(nums, index - 1, subset, target))
                    {
                        return true;
                    }
                    subset[i] -= selected; // We have to reverse the previous action
                }
            }
            return false;
        }
        #endregion
        #region Leetcode 5  Longest Palindromic Substring
        public int GetLength(int start, int end, string str)
        {
            while (start >= 0 && end < str.Length && str[start] == str[end])
            {
                --start;
                ++end;
            }
            return end - start - 1; // This is inclusive on both sides so then we have to minue one
        }
        public string LongestPalindrome(string s)
        {
            int start = 0;
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int cur = Math.Max(GetLength(i, i, s), GetLength(i, i + 1, s));
                if (cur > len)
                {
                    len = cur;
                    start = i - (len - 1) / 2;
                }
            }
            return s.Substring(start, len);
        }
        #endregion
        #region Leetcode 152  Maximum Product Subarray
        public int MaxProduct(int[] nums)
        {
            int n = nums.Length;
            if (n == 0 || nums == null) { return 0; }
            int max = nums[0];
            int min = nums[0];
            int result = nums[0];
            for (int i = 1; i < n; i++)
            {
                int cur = nums[i];
                int r1 = cur * min;
                int r2 = cur * max;
                min = Math.Min(Math.Min(r1, r2), cur);
                max = Math.Max(Math.Max(r1, r2), cur);
                result = Math.Max(result, max);
            }
            return result;
        }
        #endregion
        #region Leetcode 300  Longest Increasing Subsquence
        public int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            if (n == 0 || nums == null) { return 0; }
            int[] dp = new int[n];
            // dp[i] means the LIS that can be obtained by using the first i elements in nums
            Array.Fill(dp, 1); // Note that a number itself is considered as a subsquence
            int max = 1;
            for (int i = 0; i < n; i++)
            {
                int len = 1;
                int cur = nums[i];
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < cur)
                    {
                        len = Math.Max(len, dp[j] + 1);
                    }
                }
                dp[i] = len;
                if (len > max) { max = len; }
            }
            return max;
        }
        #endregion
        #region Leetcode 15  3 Sum
        // O(n2) Time Complexity
        // We use a double pointer to a 2 Sum
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            IList<IList<int>> ans = new List<IList<int>>();
            for (int i = 0; i < n-2; i++) // We need to at least space out two numbers
            {
                int cur = nums[i];
                if (cur > 0) { break; }// There is no way that we are going to find two positive numbers that have the a negative sum
                else if (i > 0 && cur == nums[i - 1]) { continue; }// Avoid same value
                int target = -cur;
                int l = i + 1;
                int r = n - 1;
                while (l < r)
                {
                    int sum = nums[l] + nums[r];
                    if(sum== target)
                    {
                        ans.Add(new List<int>() { cur, nums[l], nums[r] });
                        // The following steps must be done to avoid Adding the same combination twice
                        // Note that there could be multiple combinations
                        ++l;
                        while(l<r&&nums[l] == nums[l - 1]) { ++l; }
                        --r;
                        while(l<r && nums[r] == nums[r + 1]) { --r; }
                    }
                    else if (sum < target) // We need to find a larger value
                    {
                        ++l;
                    }
                    else { --r; }
                }
                
            }
            return ans;
        }
        #endregion
    }
}
