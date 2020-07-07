using System;
using System.Collections.Generic;

namespace DP_Decision_Making_Problems
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 638  Shopping Offers
        public int OriginalPrice(IList<int> needs, IList<int> prices)
        {
            int sum = 0;
            for (int i = 0; i < needs.Count; i++)
            {
                sum += needs[i] * prices[i];
            }
            return sum;
        }
        public int Shopping(int index, IList<int> prices, IList<IList<int>> special, IList<int> needs)
        {
            if (index == special.Count) { return OriginalPrice(needs, prices); } // We have no more special offer to use
            IList<int> offer = special[index];
            List<int> clone = new List<int>();
            int i;
            for (i = 0; i < offer.Count - 1; i++) // Note that the last value of this list is actually the price
            {
                int need = needs[i] - offer[i];
                if (need < 0) { break; } // We are buying more than we need
                else { clone.Add(need); } // We are able to take this coupon
            }
            int next = Shopping(index + 1, prices, special, needs); // This is the money we need if we do not use this special offer
            if (i == offer.Count - 1) // We are able to use all of them
            {
                return Math.Min(next, offer[i] + Shopping(index, prices, special, clone)); // Note that we cannot add one to the index
                // because we might be able to use this coupon multiple times
            }
            return next;
        }
        public int ShoppingOffers(IList<int> price, IList<IList<int>> special, IList<int> needs)
        {
            return Shopping(0, price, special, needs);
        }
        #endregion
        #region Leetcode 198/213  House Robber Series
        public int Rob(int[] nums)
        {
            int n = nums.Length;
            if (n == 0) { return 0; }
            else if (n == 1) { return nums[0]; }
            else if (n == 2) { return Math.Max(nums[0], nums[1]); }
            return Math.Max(RobFind(0, n - 1, nums), RobFind(1, n, nums));
        }
        public int RobFind(int start, int end, int[] nums)
        {
            int dp2 = nums[start];
            int dp1 = Math.Max(nums[start + 1], dp2);
            for (int i = start + 2; i < end; i++)
            {
                int dp = Math.Max(dp2 + nums[i], dp1);
                dp2 = dp1;
                dp1 = dp;
            }
            return dp1;
        }
        #endregion
        #region Leetcode 322  Coin Change
        public int CoinChange(int[] coins, int amount)
        {
            int max = amount + 1;
            int[] dp = new int[max];
            //dp[i] denotes the least amount of coins you need to pay "i" dollar
            Array.Fill(dp, max);
            dp[0] = 0;
            for (int i = 1; i <= amount; i++)
            {
                foreach (int coin in coins)
                {
                    if (coin <= i)// We are able to use this coin
                    {
                        dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                    }
                }
            }
            return (dp[amount] >= max) ? -1 : dp[amount];
        }
        #endregion
        #region Leetcode 1024  Video Stitching
        public int VideoStitching(int[][] c, int T)
        {
            int n = c.Length;
            int ans = 0;
            int start = 0;
            int end = 0;
            int index = 0;
            (int, int)[] clips = new (int, int)[n];
            for (int i = 0; i < n; i++)
            {
                clips[i] = (c[i][0], c[i][1]);
            }
            Array.Sort(clips, (p1, p2) => p1.Item1.CompareTo(p2.Item1)); //Sorting based on videos' start time
            while (end < T)
            {
                while (index < n && clips[index].Item1 <= start)// This should stay in cover range of the last video
                {
                    end = Math.Max(end, clips[index].Item2);
                    //We Should extend as much as possible
                    ++index;
                }
                if (start == end) { return -1; }// We can no longer extend
                start = end; // The new start should be the current end
                ++ans;
            }
            return ans;
        }
        #endregion
        #region Leetcode 1402  Reducing Dishes
        // Naive Recursion with mem approach
        public int MaxSatisfaction(int[] satisfaction)
        {
            int n = satisfaction.Length;
            Array.Sort(satisfaction); // Note that the order of the dishes can be changed
            maxsat = new int[n][];
            for (int i = 0; i < n; i++)
            {
                maxsat[i] = new int[n + 1];
                Array.Fill(maxsat[i], -1);
            }
            return satFind(0, 1, satisfaction);
        }
        int[][] maxsat;
        public int satFind(int index, int time, int[] sat)
        {
            if (index >= sat.Length) { return 0; }
            else if (maxsat[index][time] != -1) { return maxsat[index][time]; }
            return maxsat[index][time] = Math.Max(satFind(index + 1, time, sat),
                satFind(index + 1, time + 1, sat) + time * sat[index]);
        }
        #endregion
    }
}
