using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace DP
{
    class DP
    {
        static void Main(string[] args)
        {
            Console.WriteLine('c' ^'c' ^ 'b');
        }
        /// <summary>
        /// Returns whether the current block is legal to visit
        /// </summary>
        private bool CanVisit(int m, int n, int x, int y)
        {
            return x < n && x >= 0 && y >= 0 && y < m;
        }
        #region Leetcode 121/122/123/309/714  Best time to buy and sell stocks
        #region Leetcode 121
        // Thoughts : The ultimate goal is to find the lowest valley prior to the largest peak
        public int MaxProfitI(int[] prices)
        {
            int minPrice = int.MaxValue; // Lowest valley
            int maxProfit = 0; // Largest peak
            int n = prices.Length;
            if (n == 0 || n == 1) { return 0; }
            for (int i = 0; i < n; ++i)
            {
                if (prices[i] < minPrice)
                {
                    minPrice = prices[i];
                }
                // Use else here because buying and selling on the same day returns no profit
                else if (maxProfit < prices[i] - minPrice)
                {
                    maxProfit = prices[i] - minPrice;
                }
            }
            return maxProfit;
        }
        #endregion

        #region Leetcode 122
        // Thoughts : We should do as much as profitable transactions as possible 
        // Solution : Greedy
        public int MaxProfitII(int[] prices)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int buy = 0; int sell = 0; int profit = 0;
            while (buy < n && sell < n)
            {
                while (buy + 1 < n && prices[buy + 1] < prices[buy])
                {
                    ++buy;
                }
                sell = buy;
                while (sell + 1 < n && prices[sell + 1] > prices[sell])
                {
                    ++sell;
                }
                profit += prices[sell] - prices[buy];
                buy = sell + 1;
            }
            return profit;
        }
        #endregion

        #region Leetcode 123
        public int MaxProfitIII_BruteForce(int[] prices)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int k = 2;
            int[,] dp = new int[k + 1, n];
            for (int i = 0; i < n; i++)
            {
                dp[0, i] = 0;
            }
            for (int i = 0; i < k; i++)
            {
                dp[i, 0] = 0;
            }
            for (int i = 1; i <= k; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    int max = 0;
                    for (int m = 0; m < j; m++)
                    {
                        max = Math.Max(max, dp[i - 1, m] + prices[j] - prices[m]);
                    }
                    dp[i, j] = Math.Max(dp[i, j - 1], max);
                }
            }
            return dp[k, n - 1];
        }
        /* Optimization Explanation
         * dp[i,j] = max(dp[i,j-1], max(prices[j] - prices[m] + dp[i-1,m])) where m is 0...j-1
         * 
         * When i = 2 and j = 3
         * m = 0   prices[3] - prices[0] + dp[1,0]
         * m = 1   prices[3] - prices[1] + dp[1,1]
         * m = 2   prices[3] - prices[2] + dp[1,2]
         * 
         * Since prices[j] is fixed, our goal is to find the maxDiff between dp[i-1,m] and prices[m]
         * 
         * When i = 2 and j = 4
         * 
         * m = 0   prices[4] - prices[0] + dp[1,0]
         * m = 1   prices[4] - prices[1] + dp[1,1]
         * m = 2   prices[4] - prices[2] + dp[1,2]
         * 
         * m = 3   prices[4] - prices[3] + dp[1,3]
         * 
         * If we know the maxDiff of (i = 2, j = 3), we just have to compare it with (- prices[3] + dp[1,3])
         */
        public int MaxProfit(int[] prices)
        {
            int n = prices.Length;
            if (n == 0) { return 0; }
            int k = 2;
            int[,] dp = new int[k + 1, n];
            for (int i = 0; i <= k; i++)
            {
                int maxdiff = -prices[0];
                for (int j = 1; j < n; j++)
                {
                    dp[i, j] = Math.Max(dp[i, j - 1], maxdiff + prices[j]);
                    maxdiff = Math.Max(maxdiff, dp[i - 1, j] - prices[j]);
                }
            }
            return dp[k, n - 1];

        }
        #endregion

        #region Leetcode 309
        // Thoughts : States
        // We have to "vacant" before "buy"
        // We have to "buy" before "sell"
        public int MaxProfitwithCooldown(int[] prices)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int vacant = 0;
            // Doing nothing
            int buy = -prices[0];
            // Holding a stock
            int sell = 0;
            // Selling a stock
            for (int i = 1; i < n; ++i)
            {
                int cur_buy = Math.Max(vacant - prices[i]/* buy after sell*/, buy);
                int cur_sell = buy + prices[i]; // sell after buy
                int cur_vacant = Math.Max(sell, vacant);
                vacant = cur_vacant;
                buy = cur_buy;
                sell = cur_sell;
            }
            return Math.Max(vacant, sell);
            // Buying on the last day is not a wise solution
        }
        #endregion

        #region Leetcode 714 
        public int MaxProfitwithFee(int[] prices, int fee)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int[][] dp = new int[n][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[2];
            }
            // dp [i][0] means having no stock in hand on the ith day while 1 means having a stock
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
        #endregion
        #region Leetcode 115  Distinct Subsequence
        public int NumDistinct(string s, string t)
        {
            //dp[i][j]: The number of subsequence in s[0:j] that equals t[0-i];
            int ls = s.Length;
            int lt = t.Length;
            long[][] dp = new long[lt + 1][];
            for (int i = 0; i < lt + 1; i++)
            {
                dp[i] = new long[ls + 1];
            }
            Array.Fill(dp[0], 1); // If the target is empty, then there is only one solution
            for (int i = 1; i <= lt; ++i)
            {
                for (int j = 1; j <= ls; ++j)
                {
                    if (s[j - 1] == t[i - 1])
                    {
                        dp[i][j] = dp[i][j - 1] // Skip s[j]
                            + dp[i - 1][j - 1]; // Match s[j] with t[i]
                    }
                    else { dp[i][j] = dp[i][j - 1]; } // Skip s[j]
                }
            }
            return (int)(dp[lt][ls]);
        }
        #endregion
        #region Leetcode 64  Minimum Path Sum
        public int MinPathSum(int[][] grid)
        {
            int y = grid.Length;
            if (y == 0) { return 0; }
            int x = grid[0].Length;
            int[][] dp = new int[y][];
            for (int i = 0; i < y; i++)
            {
                dp[i] = new int[x];
            }
            dp[0][0] = grid[0][0];
            for (int i = 1; i < x; i++)
            {
                dp[0][i] = dp[0][i - 1] + grid[0][i];
            }
            for (int i = 1; i < y; i++)
            {
                dp[i][0] = dp[i - 1][0] + grid[i][0];
            }
            for (int i = 1; i < y; i++)
            {
                for (int j = 1; j < x; j++)
                {
                    dp[i][j] = Math.Min(dp[i - 1][j], dp[i][j - 1]) + grid[i][j];
                }
            }
            return dp[y - 1][x - 1];
        }
        #endregion
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
        // We use a double pointer to do a 2 Sum
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            IList<IList<int>> ans = new List<IList<int>>();
            for (int i = 0; i < n - 2; i++) // We need to at least space out two numbers
            {
                int cur = nums[i];
                if (cur > 0) { break; }// There is no way that we are going to find two positive numbers that have the a negative sum
                else if (i > 0 && cur == nums[i - 1]) { continue; }// Avoid the same value as the last one
                int target = -cur;
                int l = i + 1;
                int r = n - 1;
                // Find a two sum that add up to the target, which is -cur
                while (l < r)
                {
                    int sum = nums[l] + nums[r];
                    if (sum == target)
                    {
                        ans.Add(new List<int>() { cur, nums[l], nums[r] });
                        // The following steps must be done to avoid adding the same combination twice
                        // Note that there could be multiple combinations
                        ++l;
                        while (l < r && nums[l] == nums[l - 1]) { ++l; }
                        --r;
                        while (l < r && nums[r] == nums[r + 1]) { --r; }
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
        #region Leetcode 1143  Longest Common Subsequence
        public int LongestCommonSubsequence(string text1, string text2)
        {
            int n = text1.Length;
            int m = text2.Length;
            int[][] dp = new int[n + 1][];
            for (int i = 0; i <= n; ++i)
            {
                dp[i] = new int[m + 1];
                //dp[i][j] stores the LCS using text1[0-i] and text2[0-j]
            }
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= m; ++j)
                {
                    if (text2[j - 1] == text1[i - 1])// Note that the index must minue one
                    // If we are able to use this char
                    {
                        dp[i][j] = dp[i - 1][j - 1] + 1;
                        // We expand one tile
                    }
                    else { dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]); }// Then the answer should come fromthe previous text1 or text2
                }
            }
            return dp[n][m];
        }
        public int FindLength(int[] A, int[] B)
        {
            int l1 = A.Length;
            int l2 = B.Length;
            int[,] dp = new int[l1 + 1, l2 + 1];
            int max = 0;
            for (int i = 1; i <= l1; ++i)
            {
                for (int j = 1; j <= l2; ++j)
                {
                    if (A[i - 1] == B[j - 1])
                    {
                        dp[i, j] = Math.Max(dp[i, j], dp[i - 1, j - 1] + 1);
                        max = Math.Max(dp[i, j], max);
                    }

                }
            }
            return max;
        }
        #endregion
        #region Leetcode 84  Largest Rectangle in a Histogram
        // Our Ultimate goal for this question is to maintain a monetone increasing stack
        public int LargestRectangleArea(int[] heights)
        {
            int max = 0;
            Stack<int> s = new Stack<int>();
            // The index of the numbers are stored instead of the values
            s.Push(-1);
            // We put a buffer value;
            int n = heights.Length;
            for (int i = 0; i < n; i++) // Try to put every element in the stack
            {
                while (s.Peek() != -1 && heights[s.Peek()] >= heights[i])
                {
                    max = Math.Max(max, heights[s.Pop()] * (i - s.Peek()/*Exclusive*/- 1));
                }
                s.Push(i);
            }
            while (s.Peek() != -1)
            {
                max = Math.Max(max, heights[s.Pop()] * (n - s.Peek() - 1));
            }
            return max;
        }
        #endregion
        #region Leetcode 91  Decode ways
        public int NumDecodings(string s)
        {
            int n = s.Length;
            if (n == 0 || s == null || s[0] == '0') { return 0; }
            else if (n == 1) { return 1; }
            // The number that starts with an zero is illegal
            int[] dp = new int[n];
            dp[0] = 1; // When there is only one character and it is not '0', there is one decode way
            for (int i = 1; i < n; i++)
            {
                bool r1 = Valid(s[i]);
                bool r2 = Valid(s[i - 1], s[i]);
                if (!r1 && !r2)
                {
                    return 0;
                    // We cannot decode the string
                }
                if (r1)
                {
                    dp[i] += dp[i - 1];
                }
                if (r2)
                {
                    dp[i] += (i >= 2) ? dp[i - 2] : 1;
                }
            }
            return dp[n - 1];
        }
        public bool Valid(char a)
        {
            return a != '0';
        }
        public bool Valid(char a, char b)
        {
            int value = Convert.ToInt32(a - '0') * 10 + Convert.ToInt32(b - '0');
            return value >= 10 && value <= 26;
        }
        #endregion
        #region Leetcode 279  Perfect Squares 
        public int NumSquares(int n)
        {
            int[] dp = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                dp[i] = i;
                // Foreach i, we can use i amount of one's
            }
            for (int i = 2; i <= n; i++)
            {
                for (int j = 0; j * j <= i; j++) // Enumerate every possibility
                {
                    dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
                }
            }
            return dp[n];
        }
        #endregion
        #region Leetcode 1320   Minimum Distance to Type a Word Using Two Fingers
        // Once the best solution to type word[0-i] is found, we only care about the last position of two fingers 
        // Therefore, we only need to record the position of the last finger because another finger is always on word[i-1]
        public int MinimumDistance(string word)
        {
            int n = word.Length;
            MDdp = new int[n][];
            for (int i = 0; i < n; i++)
            {
                MDdp[i] = new int[27];
                Array.Fill(MDdp[i], int.MinValue);
            }
            return MDfind(0, 26, word);
        }
        int[][] MDdp;
        int kRest = 26;
        public int MDfind(int index, int other, string word)
        {
            if (index == word.Length) { return 0; }// We have printed out the whole word
            else if (MDdp[index][other] != int.MinValue) { return MDdp[index][other]; }
            int prev = index == 0 ? kRest : word[index - 1] - 'A'; // Note that the another finger must be on word[i-1]
            int cur = word[index] - 'A';
            return MDdp[index][other] = Math.Min(MDfind(index + 1, other, word) + dist_cost(prev, cur)/*Using the same finger*/
                , MDfind(index + 1, prev, word) + dist_cost(other, cur)/*Using the other finger*/);
        }
        public int dist_cost(int p1, int p2)
        {
            if (p1 == kRest) { return 0; }// It is not on the keyboard yet
            return Math.Abs(p1 / 6 - p2 / 6) + Math.Abs(p1 % 6 - p2 % 6);
        }

        #endregion
        #region Leetcode 1048  Longest String Chain
        public int LongestStrChain(string[] words)
        {
            Dictionary<string, int> dp = new Dictionary<string, int>();
            Array.Sort(words, (p1, p2) => { return p1.Length.CompareTo(p2.Length); });
            int max = 0;
            foreach (string word in words)
            {
                dp[word] = 1;
                for (int i = 0; i < word.Length; i++) // We try to cut every char of the word
                {
                    string pred = word.Remove(i, 1);
                    if (dp.ContainsKey(pred) && dp[word] < dp[pred] + 1) // Update the largest value
                    {
                        dp[word] = dp[pred] + 1;
                    }
                }
                max = Math.Max(max, dp[word]);
            }
            return max;
        }
        #endregion
        #region Leetcode 1458  Max Dot Product of Two Subsequences
        public int MaxDotProduct(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;
            // The dp array has a padding
            int[][] dp = new int[n1 + 1][];
            // dP[i][j]: the maximum dot product of two subsequences 
            // starting in the position i of nums1 and position j of nums2.
            for (int i = 0; i <= n1; i++)
            {
                dp[i] = new int[n2 + 1];
                Array.Fill(dp[i], int.MinValue / 2);
            }
            for (int i = 1; i <= n1; i++)
            {
                for (int j = 1; j <= n2; j++)
                {
                    dp[i][j] = Math.Max(Math.Max(0, dp[i - 1][j - 1]) + 
                        nums1[i - 1] * nums2[j - 1], // Using the current combo 
                        Math.Max(dp[i - 1][j], dp[i][j - 1]));
                }
            }
            return dp[n1][n2];
        }
        #endregion
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
            // The after need after using the current coupon
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
            return maxsat[index][time] = Math.Max(satFind(index + 1, time, sat)/*Skipping the dish*/,
                satFind(index + 1, time + 1, sat) + time * sat[index]/*Making the dish*/);
        }
        #endregion
        #region Leetcode 1105  Filling Bookcase shelves
        public int MinHeightShelves(int[][] books, int shelf_width)
        {
            int n = books.Length;
            FBSdp = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                FBSdp[i] = new int[shelf_width + 1];
                Array.Fill(FBSdp[i], -1);
            }
            return FBSfind(0, books, shelf_width, 0, 0);

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
            if (cur + length <= limit) // If we are able to put the current book on the same level as the last one
            {
                int new_height = Math.Max(height, width);
                int dif = new_height - height;
                // This checks if the current book is higher than the previous books on the shelf
                // If so, we need to add this additional part because the height of the layer is determined by the highest book
                same_level = dif + FBSfind(index + 1, books, limit, new_height, cur + length);
            }
            diff_level = width + FBSfind(index + 1, books, limit, width, length);
            // The current length is now the length of the book because a new level is created
            // The current height is now 
            return FBSdp[index][cur] = Math.Min(same_level, diff_level);
        }
        #endregion
        #region Leetcode 375  Guess Number Higher or Lower II
        // This solution actually resembles Binary Search
        public int GetMoneyAmount(int n)
        {
            guessNumberdp = new int[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                guessNumberdp[i] = new int[n + 1];
            }
            return GuessNumberII(n, 1);
        }
        int[][] guessNumberdp;
        public int GuessNumberII(int high, int low)
        {
            if (low >= high) { return 0; }
            if (guessNumberdp[high][low] != 0) { return guessNumberdp[high][low]; }
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
        #region Leetcode 983  Minimum Cost For Tickets
        public int MincostTickets(int[] days, int[] costs)
        {
            Array.Fill(MCTdp, -1);
            return MCTfind(1, days, costs);
        }
        int[] MCTdp = new int[366];
        public int MCTfind(int count, int[] days, int[] costs)
        {
            if (count > 365)
            {
                return 0;
            }
            else if (MCTdp[count] != -1)
            {
                return MCTdp[count];
            }
            if (!ArrayContains(days, count)) { return MCTdp[count] = MCTfind(count + 1, days, costs); }
            return MCTdp[count] = Math.Min(Math.Min(MCTfind(count + 1, days, costs) + costs[0], MCTfind(count + 7, days, costs) + costs[1]),
                MCTfind(count + 30, days, costs) + costs[2]);

        }
        public bool ArrayContains(int[] nums, int target)
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = nums[m];
                if (cur == target) { return true; }
                else if (cur > target) { r = m; }
                else { l = m + 1; }
            }
            return false;
        }
        #endregion
        #region Leetcode 740  Delete and Earn
        public int DeleteAndEarn(int[] nums)
        {
            // This question resembles the house robber because when you take the current number
            // You cannot take the next one
            if (nums.Length == 0) { return 0; }
            int[] sum = new int[nums.Max() + 1];
            foreach (var item in nums)
            {
                sum[item] += item;
            }
            return DAErob(sum);
        }
        public int DAErob(int[] nums)
        {
            int dp1 = 0;
            int dp2 = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int dp = Math.Max(dp2 + nums[i], dp1);
                dp2 = dp1;
                dp1 = dp;
            }
            return dp1;
        }
        #endregion
        #region Leetcode 62/62  Unique Path Series
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int n = obstacleGrid.Length;
            if (obstacleGrid == null || n == 0) { return 0; }
            int m = obstacleGrid[0].Length;
            UPmem = new int[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                UPmem[i] = new int[m + 1];
                Array.Fill(UPmem[i], -1);
            }
            return UPFind(obstacleGrid, 0, 0);
        }
        int[][] UPmem;
        public int UPFind(int[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length) { return 0; }
            else if (x == grid[0].Length - 1 && y == grid.Length - 1) { return 1 - grid[y][x]; } // It is 1-grid[y][x] here
            // There is a possibility that the destination is also an obstacle
            else if (UPmem[y][x] != -1) { return UPmem[y][x]; } // We have already calculated this 
            else if (grid[y][x] == 1) { return 0; }
            return UPmem[y][x] = UPFind(grid, x + 1, y) + UPFind(grid, x, y + 1);
        }
        #endregion
        #region Leetcode 1340  Jump Game V
        public int MaxJumps(int[] arr, int d)
        {
            int n = arr.Length;
            max = d;
            // This represents the largest distance the figure can jump to
            mjdp = new int[n];
            int ans = 0;
            for (int i = 0; i < n; i++) // We find every starting point and then call the dp function
            {
                ans = Math.Max(ans, MJFind(i, arr));
            }
            return ans;
        }
        int max;
        int[] mjdp;
        public int MJFind(int index, int[] nums)
        {
            if (mjdp[index] != 0) { return mjdp[index]; }// If already calculated
            int ans = 1; // It is set to one because doing nothing is considered one step
            for (int i = index + 1; i < Math.Min(nums.Length, index + max + 1) && nums[index] > nums[i]/*Making sure that the player can cross*/; i++)
            // We first look at the right side of the index
            // This loop must also make sure that the jumping range do not surpass index + max or index - max
            {
                ans = Math.Max(ans, MJFind(i, nums) + 1);
                // Adding one meaning making a move
            }
            for (int i = index - 1; i >= Math.Max(0, index - max) && nums[index] > nums[i]; i--)
            // We then search the left side of the index
            {
                ans = Math.Max(ans, MJFind(i, nums) + 1);
            }
            return mjdp[index] = ans;
        }
        #endregion
        #region Leetcode 576  Out of Boundary Paths
        public static int[][][] OBdp;
        public const int mod = 1000000007;
        private int Find(int x, int y, int k, int m, int n)
        {
            if (x < 0 || y < 0 || y >= n || x >= m) { return 1; } // We successfully reached the boundary
            else if (OBdp[x][y][k] != -1) { return OBdp[x][y][k] % mod; }// We already calculated it 
            else if (k == 0) { return 0; } // We used all of our moves
            // We extend to four ways
            int tempVal = 0;
            tempVal = (tempVal + Find(x, y - 1, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x - 1, y, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x + 1, y, k - 1, m, n)) % mod;
            tempVal = (tempVal + Find(x, y + 1, k - 1, m, n)) % mod;
            OBdp[x][y][k] = tempVal;
            return tempVal % mod;
        }
        public int FindPaths(int m, int n, int N, int x, int y)
        {
            // Here m is the x boundary
            // And n is the y boundary
            // There is a little problem with the description of the second example
            OBdp = new int[m][][];
            for (int i = 0; i < m; ++i)
            {
                OBdp[i] = new int[n][];
                for (int j = 0; j < n; ++j)
                {
                    OBdp[i][j] = new int[N + 1];
                    Array.Fill(OBdp[i][j], -1);
                }
            }
            return Find(x, y, N, m, n);
        }
        #endregion
        #region Leetcode 72  Edit Distance
        // Our goal is edit both words until one of their length is zero
        public int MinDistance(string w1, string w2)
        {
            word1 = w1;
            word2 = w2;
            int l1 = w1.Length;
            int l2 = w2.Length;
            MEDdp = new int[l1 + 1][];
            // d[i][j] stores the Minimum Edit Distance from word1[0-i] to word2[0-j]
            for (int i = 0; i < l1 + 1; i++)
            {
                MEDdp[i] = new int[l2 + 1];
                Array.Fill(MEDdp[i], -1);
            }
            return MEDfind(l1, l2);

        }
        int[][] MEDdp;
        string word1;
        string word2;
        public int MEDfind(int i1, int i2) // These two params indicates the length of the remaining word
        {
            if (i1 == 0)
            {
                return i2;
            }
            else if (i2 == 0)
            {
                return i1;
            }
            // We only need the (remaining length of another word) steps by deleting
            else if (MEDdp[i1][i2] != -1)
            {
                return MEDdp[i1][i2];
            }
            int result = 0;
            if (word1[i1 - 1] == word2[i2 - 1])// If the next character is the same, then we can skip it
            {
                result = MEDfind(i1 - 1, i2 - 1);
            }
            else
            {
                result = Math.Min(Math.Min(MEDfind(i1 - 1, i2 - 1), MEDfind(i1 - 1, i2)), MEDfind(i1, i2 - 1));
                // Replace: If we replace a character, the next character is the same. Then we can skip it
                // Delete : If we delete a character in word1, then  we can skip a character in word1
                // Insert：If we insert a character into word1, then we can skip a character in word2
                // while the length of word1 remains the same(skip one insert one)
            }
            return MEDdp[i1][i2] = result;
        }
        #endregion
        #region Leetcode 79  Word Search
        public bool Exist(char[][] board, string word)
        {
            int n = board.Length;
            if (n == 0) { return false; }
            int m = board[0].Length;
            char[] search = word.ToCharArray();
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (board[i][j] == search[0])
                    {
                        if (Search(j, i, 0, search, ref board))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool Search(int x, int y, int index, char[] search, ref char[][] board)
        {
            if (y < 0 || y >= board.Length || x < 0 || x >= board[0].Length || board[y][x] != search[index])
            {
                return false;
            }
            else if (index == search.Length - 1) { return true; } // We successfully searched through the entire word
            else
            {
                char cur = board[y][x];
                board[y][x] = '$'; // Mark as visited
                bool result = Search(x + 1, y, index + 1, search, ref board) ||
                    Search(x - 1, y, index + 1, search, ref board) ||
                    Search(x, y - 1, index + 1, search, ref board) ||
                    Search(x, y + 1, index + 1, search, ref board);
                board[y][x] = cur; // Reset to default;
                return result;
            }
        }
        #endregion
        #region Cherry Pickup Series
        #region Leetcode 741  Cherry Pickup
        public int CherryPickup(int[][] grid)
        {
            int n = grid.Length;
            CPdp = new int[n][][];
            for (int i = 0; i < n; i++)
            {
                CPdp[i] = new int[n][];
                for (int j = 0; j < n; j++)
                {
                    CPdp[i][j] = new int[n];
                    Array.Fill(CPdp[i][j], int.MinValue);
                }
            }
            return Math.Max(0, CPfind(n - 1, n - 1, n - 1, grid));
        }
        int[][][] CPdp;
        public int CPfind(int x1, int y1, int x2, int[][] grid)
        {
            int y2 = x1 + y1 - x2;
            // They started at the same pointer and move same steps
            if (y2 < 0 || y1 < 0 || x1 < 0 || x2 < 0)
            // These two figures started from bottom right and they can only move up or left
            {
                return -1;
            }
            else if (grid[y1][x1] == -1 || grid[y2][x2] == -1)
            // They all met an obstacle
            {
                return -1;
            }
            else if (x1 == 0 && y1 == 0) // They reached an end
            {
                return grid[0][0];
                // Return the value itself because the end might be an obstacle,cherry or nothing
            }
            else if (CPdp[x1][y1][x2] != int.MinValue)
            {
                return CPdp[x1][y1][x2];
            }
            int res = Math.Max(Math.Max(CPfind(x1 - 1, y1, x2 - 1, grid), CPfind(x1 - 1, y1, x2, grid)),
                Math.Max(CPfind(x1, y1 - 1, x2 - 1, grid), CPfind(x1, y1 - 1, x2, grid)));
            if (res < 0)
            // All paths have been blocked
            {
                return CPdp[x1][y1][x2] = -1;
            }
            res += grid[y1][x1];
            if (x1 != x2)
            // Note that the same cherry cannot be picked up twice
            {
                res += grid[y2][x2];
            }
            return CPdp[x1][y1][x2] = res;
        }
        #endregion
        #region Leetcode 1463  Cherry Pickup Two
        public int CherryPickupII(int[][] grid)
        {
            int n = grid.Length;
            if (n == 0 || grid == null) { return 0; }
            int m = grid[0].Length;
            CPdpII = new int[m][][];
            for (int i = 0; i < m; i++)
            {
                CPdpII[i] = new int[m][];
                for (int j = 0; j < m; j++)
                {
                    CPdpII[i][j] = new int[n];
                    Array.Fill(CPdpII[i][j], -1);
                }
            }
            return CPfindII(0, m - 1, 0, grid);

        }
        int[][][] CPdpII;
        public int CPfindII(int x1, int x2, int y, int[][] grid)
        {
            if (y < 0 || y >= grid.Length || x1 < 0 || x1 >= grid[0].Length || x2 < 0 || x2 >= grid[0].Length)
            {
                return 0;
            }
            else if (CPdpII[x1][x2][y] != -1)
            {
                return CPdpII[x1][x2][y];
            }
            int future = int.MinValue;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // Use for loop to simulate going to all different directions
                    future = Math.Max(future, CPfindII(x1 + i, x2 + j, y + 1, grid));
                }
            }
            int cur = grid[y][x1];
            if (x1 != x2)
            // If two robots are not standing on the same spot
            {
                cur += grid[y][x2];
            }
            return CPdpII[x1][x2][y] = future + cur;
        }

        #endregion
        #endregion
        #region Leetcode 368  Largest Divisible Subset
        public IList<int> LargestDivisibleSubset(int[] nums)
        {
            Array.Sort(nums);

            int n = nums.Length;
            if (n == 0)
            {
                return new List<int>();
            }
            int[] dp = new int[n];
            // dp[i] stores the largest divisible subset ending with nums[i]
            int[] next = new int[n];

            int len = 0;
            int index = -1;

            for (int i = 0; i < n; i++)
            {
                dp[i] = 1; next[i] = -1;
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] % nums[j] == 0)
                    {
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
                        if (dp[i] == dp[j] + 1)
                        // If including j is the current best solution
                        {
                            next[i] = j;
                            // ... then we mark the next number of i as j
                        }
                    }
                }
                if (dp[i] > len)
                {
                    len = dp[i];
                    index = i;
                }
            }
            IList<int> ans = new List<int>();
            while (index != -1)
            {
                ans.Add(nums[index]);
                index = next[index];
            }
            return ans;
        }
        #endregion
        #region Leetcode 120  Triangle
        public int MinimumTotal(IList<IList<int>> t)
        {
            int line = t.Count;
            if (line == 0) { return 0; }
            else if (line == 1) { return t[0][0]; }

            int[][] triangle = new int[line][];
            for (int i = 0; i < line; i++)
            {
                triangle[i] = t[i].ToArray();
            }

            for (int i = line - 2; i >= 0; i--) // From the penultimate column to the top
            {
                for (int j = 0; j < triangle[i].Length; j++) // From left to right
                {
                    triangle[i][j] = Math.Min(triangle[i + 1][j + 1], triangle[i + 1][j]) + triangle[i][j];
                    // triangle[i][j]: the minimum path sum from the bottom column to the top
                }
            }

            return triangle[0][0];
        }
        #endregion
        #region Leetcode 174  Dungeon Game
        public int CalculateMinimumHP(int[][] dungeon)
        {
            int n = dungeon.Length;
            int m = dungeon[0].Length;
            int[,] hp = new int[n + 1, m + 1];
            // hp[i,j] is the minimum health needed to going from the destination to dungeon[i][j]
            for (int i = 0; i <= n; ++i)
            {
                for (int j = 0; j <= m; ++j)
                {
                    hp[i, j] = int.MaxValue;
                }
            }
            hp[n, m - 1] = 1;
            hp[n - 1, m] = 1;
            // The character needs one hp to go through the destination

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = m - 1; j >= 0; j--)
                {
                    int need = Math.Min(hp[i + 1, j], hp[i, j + 1]) + dungeon[i][j];
                    hp[i, j] = need <= 0 ? 1 : need;
                }
            }
            return hp[0, 0];
        }
        #endregion
        #region Leetcode 70  Climbing Stairs
        public int ClimbStairs(int n)
        {
            if (n <= 2)
            {
                return n;
            }
            int dp1 = 2; // dp[i-1]
            int dp2 = 1; // dp[i-2]

            for (int i = 2; i < n; ++i)
            {
                int dp = dp1 + dp2;
                dp2 = dp1;
                dp1 = dp;
            }
            return dp1;
        }
        #endregion
        #region Word Break Series
        public bool WordBreakI(string s, IList<string> wordDict)
        {
            int n = s.Length;
            bool[] dp = new bool[n + 1];
            dp[0] = true;
            // dp[i]: whether the word dict can build s[0:i+1]
            for (int i = 1; i <= n; i++)
            {
                foreach (string word in wordDict.ToArray())
                {
                    int l = word.Length;
                    int start = i - l;

                    if (start < 0 || !dp[start]) { continue; }
                    else if (word == s.Substring(start, l))
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[n];
        }
        IList<string> ans = new List<string>();
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            WordBreakDfs(s, wordDict, "");
            return ans;
        }
        public void WordBreakDfs(string s, IList<string> words, string path)
        {
            if (WordBreakI(s, words))
            // If the current string can be separated
            {
                if (string.IsNullOrEmpty(s))
                {
                    ans.Add(path.Substring(0, path.Length - 1));
                    // Delete the ending path
                    return;
                }
                else
                {
                    for (int i = 1; i <= s.Length; i++)
                    // Try every middle point to separate the string
                    {
                        if (words.Contains(s.Substring(0, i)))
                        {
                            WordBreakDfs(s.Substring(i), words, path + s.Substring(0, i) + " ");
                        }
                    }
                }
            }
        }
        #endregion
        #region Leetcode 1218  Longest Arithmetic Subsequence of Given Difference
        public int LongestSubsequence(int[] arr, int k)
        {
            Dictionary<int, int> length = new Dictionary<int, int>();
            // length[i]: the longest arithmetic subsequence that ends with i
            int max = 1;
            foreach (int n in arr)
            {
                if (length.ContainsKey(n - k/*The previous element in the subsequence that ends with n*/))
                {
                    length[n] = length[n - k] + 1;
                }
                else
                // It is not able to form a arithmetic subsequence
                {
                    length[n] = 1;
                }
                max = Math.Max(max, length[n]);
            }
            return max;
        }
        #endregion
        #region Leetcode 221  Maximal Square
        public int MaximalSquare(char[][] matrix)
        {
            int n = matrix.Length;
            if (n == 0) { return 0; }
            int m = matrix[0].Length;

            int[,] dp = new int[n + 1, m + 1];
            // dp[i+1,j+1]: The longest side length when the right corner is matrix[i][j]   
            int max = 0;

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (matrix[i][j] == '1')
                    // If the current spot can be used as a right corner
                    {
                        dp[i + 1, j + 1] = Math.Min(dp[i, j + 1], Math.Min(dp[i + 1, j], dp[i, j])) + 1;
                        // Coming from three different directions
                        max = Math.Max(max, dp[i + 1, j + 1]);
                    }
                }
            }

            return max * max;
        }
        #endregion
        #region Leetcode 1449  Form Largest Integer With Digits That Add up to Target
        public string LargestNumber(int[] cost, int target)
        {
            string[] dp = new string[target + 1];
            // dp[i]: the answer when the target is i
            Array.Fill(dp, "0");
            dp[0] = "";
            for (int i = 1; i <= target; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    int left = i - cost[j - 1];
                    if(left < 0 || dp[left] == "0"|| dp[left].Length + 1 < dp[i].Length)
                    {
                        continue;
                    }
                    StringBuilder cur = new StringBuilder();
                    cur.Append(j).Append(dp[left]);
                    dp[i] = cur.ToString();
                }
            }
            return dp[target];
        }
        #endregion
        #region Leetcode 1416  Restore The Array
        public int NumberOfArrays(string s, int k)
        {
            RAdp = new int[s.Length];
            Array.Fill(RAdp, -1);
            return Find(s, k, 0);
        }
        int[] RAdp;
        int kmod = (int)Math.Pow(10, 9) + 7;
        public int Find(string s, int k, int index)
        {
            if (index == s.Length)
            {
                // Successfully restore the entire array
                return 1;
            }
            else if (s[index] == '0')
            {
                // There are no leading zeros in the array
                return 0;
            }
            else if (RAdp[index] != -1)
            {
                return RAdp[index];
            }

            long num = 0;
            int ans = 0;

            for (int j = index; j < s.Length; ++j)
            {
                num = num * 10 + s[j] - '0';
                // num is the value of s[index:j];
                // accumulates the digits
                if (num > k)
                {
                    break;
                }
                ans += Find(s, k, j + 1);
                ans %= kmod;
            }
            return RAdp[index] = ans;
        }
        #endregion
        #region Leetcode 1191  K-Concatenation Maximum Sum
        public int KConcatenationMaxSum(int[] arr, int k)
        {
            int kmod = (int)Math.Pow(10, 9) + 7;
            if (k < 3)
            {
                return TMaxSubarraySum(arr, k) % kmod;
            }
            var m = MaxSubarrayTwoTimes(arr);

            long sum = arr.Sum();

            return (int)Math.Max(Math.Max(m.Item1, m.Item2), (sum * (k - 2) + m.Item2) % kmod);
        }
        public int TMaxSubarraySum(int[] arr, int t)
        {
            long max = 0;
            long ans = 0;

            for (int i = 0; i < t; i++)
            {
                foreach (int n in arr)
                {
                    max = Math.Max(max + n, 0);
                    ans = Math.Max(ans, max);
                }
            }
            return (int)ans;
        }
        public (long, long) MaxSubarrayTwoTimes(int[] arr)
        {
            long max1 = 0;
            long max2 = 0;

            long running = 0;
            long max = 0;

            for (int i = 0; i < 2; i++)
            {
                foreach (int n in arr)
                {
                    running = Math.Max(running + n, 0);
                    max = Math.Max(max, running);
                }
                max1 = max;
            }
            max2 = max;
            return (max1, max2);
        }
        #endregion
        #region Leetcode 1155  Number of Dice Rolls With Target Sum
        public int NumRollsToTarget(int dices, int f, int target)
        {
            int[,] dp = new int[dices + 1, target + 1];
            // dp[i,j]: The amount of way to reach the sum of j with i dices
            dp[0, 0] = 1;
            int kmod = (int)Math.Pow(10, 9) + 7;
            for (int d = 1; d <= dices; ++d) 
            {
                for (int k = 1; k <= f; ++k) // Enumerate every possible dice face
                {
                    for (int j = k; j <= target; ++j)// Range to the target
                    {
                        dp[d, j] += dp[d - 1, j - k/*With one less dice and reach j - k with a k on this turn*/];
                        dp[d, j] %= kmod;
                    }
                }
            }
            return dp[dices, target];
        }
        #endregion
        #region Leetcode 1127  Airplane Seat Assignment Possibility
        public double NthPersonGetsNthSeat(int n)
        {
            return n == 1 ? 1.0d : 0.5d;
        }
        #endregion
        #region Leetcode 329  Longest Increasing Path
        public int LongestIncreasingPath(int[][] matrix)
        {
            m = matrix.Length;
            if(m == 0)
            {
                return 0;
            }
            n = matrix[0].Length;
            LIPdp = new int[m, n];
            int ans = 0;

            // Enumerate every possible ending points
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ans = Math.Max(ans, LIPfind(matrix, j, i));
                }
            }
            return ans;
        }
        int m;
        int n;
        /// <summary>
        /// dp[i][j]: The longest increasing path that ends at matrix[i][j]
        /// </summary>
        int[,] LIPdp;

        /// <summary>
        /// The dp function of LIP
        /// </summary>
        /// <param name="matrix">The given matrix</param>
        /// <param name="x">The current x position</param>
        /// <param name="y">The current y position</param>
        /// <returns>Returns the longest increasing path that ends at matrix[y][x]</returns>
        private int LIPfind(int[][] matrix, int x, int y)
        {
            if(LIPdp[y,x] != 0)
            {
                return LIPdp[y, x];
            }

            int result = 0, cur = matrix[y][x];
            int[] dir = new int[5] { 0, 1, 0, -1, 0 };
            for (int i = 0; i < 4; i++)
            {
                int nx = x + dir[i];
                int ny = y + dir[i + 1];
                if(CanVisit(m,n,nx,ny) && cur > matrix[ny][nx])
                {
                    result = Math.Max(result, LIPfind(matrix, x + dir[i], y + dir[i + 1]));
                }
            }
            return LIPdp[y,x] = result + 1;
        }
        #endregion
    }
}
