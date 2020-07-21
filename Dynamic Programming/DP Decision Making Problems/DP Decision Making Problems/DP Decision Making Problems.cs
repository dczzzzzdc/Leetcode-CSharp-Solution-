using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        #region Leetcode 1105  Filling Bookcase shelves
        public int MinHeightShelves(int[][] books, int shelf_width)
        {
            int n = books.Length;
            FBSdp = new int[n+1][];
            for (int i = 0; i <= n; i++)
            {
                FBSdp[i] = new int[shelf_width + 1];
                Array.Fill(FBSdp[i], -1);
            }
            return FBSfind(0, books, shelf_width, 0,0);

        }
        int[][] FBSdp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">The index of the book we are trying to position</param>
        /// <param name="books">The Array containing the information of all the books</param>
        /// <param name="limit">The width of the shelf</param>
        /// <param name="height">The current height</param>
        /// <param name="cur">The current length</param>
        /// <returns>The minimum height after positioning all the books</returns>
        public int FBSfind(int index, int[][] books, int limit, int height, int cur)
        {
            if (FBSdp[index][cur] != -1) { return FBSdp[index][cur]; } // We have calculated this circumstance
            if (index == books.Length) { return 0; } // We have finished positioning all the books
            int length = books[index][0];
            int width = books[index][1];
            int same_level = int.MaxValue; int diff_level = int.MaxValue;
            if(cur + length <= limit) // If we are able to put the current book on the same level as the last one
            {
                int new_height = Math.Max(height, width);
                int dif = new_height - height;
                // This checks if the current book is higher than the previous books on the shelf
                // If so, we need to add this additional part because the height of the layer is determined by the highest book
                same_level = dif + FBSfind(index + 1, books, limit,new_height, cur + length);
            }
            diff_level = width + FBSfind(index + 1, books, limit, width, length);
            // The current length is now the length of the book because a new level is created
            // The current height is now 
            return FBSdp[index][cur] = Math.Min(same_level, diff_level);
        }
        #endregion
        #region Leetcode 714  Best Time to Buy and Sell Stock with Transaction Fee
        public int MaxProfit(int[] prices, int fee)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int[][] dp = new int[n][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[2];
            }
            // dp [i][0] means having no stock in hand on the ith day while i means
            // having a stock
            dp[0][0] = 0;
            dp[0][1] = -prices[0];
            for (int i = 1; i < n; i++)
            {
                dp[i][0] = Math.Max(dp[i - 1][0], dp[i - 1][1] + prices[i] - fee);
                dp[i][1] = Math.Max(dp[i - 1][1], dp[i - 1][0] - prices[i]);
            }
            return Math.Max(dp[n - 1][0], dp[n - 1][1]);
        }
        #endregion
        #region 375  Guess Number Higher or Lower II
        public int GetMoneyAmount(int n)
        {
            guessNumberdp = new int[n + 1][];
            for (int i = 0; i < n+1; i++)
            {
                guessNumberdp[i] = new int[n + 1];
            }
            return GuessNumberII(n, 1);
        }
        int[][] guessNumberdp;
        public int GuessNumberII(int high, int low)
        {
            if(low >= high) { return 0; }
            if(guessNumberdp[high][low]!= 0) { return guessNumberdp[high][low]; }
            int ans = int.MaxValue;
            // Try every combination
            for (int i = low; i <= high; i++)
            {
                int cur = i + Math.Max(GuessNumberII(i - 1, low) /*The number is too big*/, GuessNumberII(high, i + 1) /* The number is too small*/);
                // We are using max here because we must take the worst situation into account
                // Also, the new range can no longer contain i
                ans = Math.Min(ans, cur);
                // Select the best decision
            }
            return guessNumberdp[high][low] = ans;
        }
        #endregion
    }
}
