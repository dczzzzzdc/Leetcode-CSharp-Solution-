using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Leetcode
{
    class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 1  Two Sum
        public int[] TwoSum(int[] nums, int target)
        {
            int n = nums.Length;
            Dictionary<int, int> supplement = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                int pair = target - nums[i];
                if (supplement.ContainsKey(pair))
                {
                    return new int[2] { i, supplement[pair] };
                }
                else if (!supplement.ContainsKey(nums[i]))
                {
                    supplement[nums[i]] = i;
                }
            }
            return new int[2] { -1, -1 };
        }
        #endregion
        #region Leetcode 2  Add Two Numbers
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode(0);
            ListNode tail = dummy;
            int sum = 0;
            while (l1 != null || l2 != null || sum != 0)
            {
                sum += ((l1 == null) ? 0 : l1.val) + ((l2 == null) ? 0 : l2.val);
                tail.next = new ListNode(sum % 10);
                tail = tail.next;
                if (l1 != null) { l1 = l1.next; }
                if (l2 != null) { l2 = l2.next; }
                sum /= 10;
            }
            return dummy.next;
        }
        #endregion
        #region Leetcode 5  Longest Palindromic String
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
            int n = s.Length;
            int start = 0;
            int len = 0;
            for (int i = 0; i < s.Length; i++) // Enumerate every possible middle point to extend
            {
                if(n - i < len / 2) // There is no way to achieve greater length
                {
                    break;
                }
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
        #region Leetcode 7  Reverse Integer
        public int Reverse(int x)
        {
            int ans = 0;
            while (x != 0)
            {
                int newAns = ans * 10 + x % 10;

                if (newAns / 10 != ans) // Check for overflow
                {
                    // int.MaxValue + 1 will be int.MinValue
                    // If it overflows, then newAns / 10 will not be equal to the previous ans
                    return 0;
                }
                ans = newAns;
                x /= 10;
            }
            return ans;
        }
        #endregion
        #region Leetcode 9  Palindrome Number
        public bool IsPalindrome(int x)
        {
            if(x < 0 || (x != 0 && x % 10 == 0))
            {
                return false;
            }
            int last_part = 0;

            while(x > last_part)
            {
                last_part = last_part * 10 + x % 10;
                x /= 10;
            }

            return x == last_part || x == last_part / 10 /* For numbers of odd length*/;
        }
        #endregion
        #region Leetcode 12  Integer to Roman
        public string IntToRoman(int num)
        {
            StringBuilder res = new StringBuilder("");
            string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] value = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int index = 0;
            // Use the big numbers at first

            while (num > 0)
            {
                int count = num / value[index];
                for (int i = 0; i < count; i++)
                {
                    res.Append(roman[index]);
                }
                num -= count * value[index];
                index++;
            }
            return res.ToString();
        }
        #endregion
        #region Leetcode 13  Roman to Integer
        public int RomanToInt(string s)
        {
            Dictionary<char, int> roman = new Dictionary<char, int>();
            roman.Add('M', 1000);
            roman.Add('D', 500);
            roman.Add('C', 100);
            roman.Add('L', 50);
            roman.Add('X', 10);
            roman.Add('V', 5);
            roman.Add('I', 1);


            int n = s.Length;
            int sum = 0;

            for (int i = 0; i < n - 1; i++)
            {
                // Normally, the string is sorted from right to left
                // Therefore, left value should be bigger or equal the right value
                if (roman[s[i]] < roman[s[i + 1]])
                {
                    sum -= roman[s[i]];
                }
                else
                {
                    sum += roman[s[i]];
                }
            }
            return sum + roman[s[s.Length - 1]];
        }
        #endregion
        #region Leetcode 15  3 Sum
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            int n = nums.Length;
            IList<IList<int>> ans = new List<IList<int>>();
            Array.Sort(nums);
            for (int i = 0; i < n - 2; i++)
            {
                int cur = nums[i];
                if(cur > 0)
                {
                    // No way to Find two positive numbers to make a negative sum
                    // Since the array is sorted, the numbers after it are also positive so the loop can break
                    break;
                }
                if(i > 0 && cur == nums[i - 1]) // Avoid the same value
                {
                    continue;
                }

                int target = -cur;
                int l = i + 1;
                int r = n - 1;

                while (l < r)
                {
                    int sum = nums[l] + nums[r];
                    if (sum == target)
                    {
                        ans.Add(new List<int>() { target, nums[l], nums[r] });

                        while (l < r && nums[l] == nums[l - 1]) { ++l; }
                        while (l < r && nums[r + 1] == nums[r]) { --r; }

                        ++l;
                        --r;
                    }
                    else if (sum > target)
                    // Shift the right pointer to the left for a smaller value
                    {
                        --r;
                    }
                    else
                    {
                        --r;
                    }
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 21  Merge Two Sorted Lists
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if(l1 == null)
            {
                return l2;
            }
            else if(l2 == null)
            {
                return l1;
            }
            ListNode dummy = new ListNode(0);
            ListNode head = dummy;
            while(l1!= null && l2!= null)
            {
                if (l1.val <= l2.val)
                {
                    head.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    head.next = l2;
                    l2 = l2.next;
                }
                head = head.next;
            }
            head.next = l1 == null ? l2 : l1;
            return dummy.next;
        }
        #endregion
        #region Leetcode 23  Merge k Sorted Lists
        public ListNode MergeKLists(ListNode[] lists)
        {
            int n = lists.Length, interval = 1;
            if(n == 0)
            {
                return null;
            }
            while(interval < n)
            {
                for (int i = 0; i < n - interval; i*=2)
                {
                    lists[i] = MergeTwoLists(lists[i], lists[i + interval]);
                }
                interval *= 2;
            }
            return lists[0];
        }
        #endregion
        #region Leetcode 50  Pow(x,n)
        public double MyPow(double x, int t)
        {
            double result = 1.0;

            long n = t;
            if (n < 0)
            {
                n = -n;
                x = 1 / x;
            }
            double cur = x;

            for (long i = n; i > 0; i /= 2)
            {
                if (i % 2 == 1)
                {
                    result *= cur;
                }
                cur *= cur;
            }
            return result;
        }
        #endregion
        #region Leetcode 53  Maximum Subarray
        public int MaxSubArray(int[] nums)
        {
            int n = nums.Length;
            if (n == 0)
            {
                return 0;
            }
            int cur = nums[0];
            int max = cur;
            for (int i = 1; i < n; ++i)
            {
                cur = Math.Max(cur + nums[i], nums[i]);
                max = Math.Max(cur, max);
            }
            return max;
        }
        #endregion
        #region Leetcode 55 Jump Game
        public bool CanJump(int[] nums)
        {
            int n = nums.Length;
            int last = n - 1; // The last valid index that can reach n - 1
            for (int i = last; i >= 0; i--)
            {
                if(i + nums[i] >= last)
                // If the current point can reach the last valid index
                {
                    last = i;
                }
            }
            return last == 0;
        }
        #endregion
        #region Leetcode 58  Length of Last Word
        public int LengthOfLastWord(string s)
        {
            int len = 0, tail = s.Length - 1;
            while(tail >= 0 && Char.IsWhiteSpace(s[tail]))
            {
                --tail;
            }
            while(tail >=0 && !Char.IsWhiteSpace(s[tail]))
            {
                --tail;
                ++len;
            }
            return len;
        }
        #endregion
        #region Leetcode 62  Unique Path
        public int UniquePaths(int x, int y)
        {
            UPdp = new int[y,x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    UPdp[i, j] = -1;
                }
            }
            return UPFind(x - 1, y - 1);
        }
        public int[,] UPdp;
        public int UPFind(int x, int y)
        {
            if (x < 0 || y < 0) { return 0; }
            else if (x == 0 && y == 0) { return 1; }
            else if (UPdp[y,x] != -1) { return UPdp[y, x]; }
            else
            {
                return UPdp[y,x] = UPFind(x - 1, y) + UPFind(x, y - 1);
            }
        }
        #endregion
        #region Leetcode 63  Unique Path II
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int n = obstacleGrid.Length;
            if (obstacleGrid == null || n == 0) { return 0; }
            int m = obstacleGrid[0].Length;
            if(obstacleGrid[n-1][m-1] == 1) { return 0; }
            UP2dp = new int[n,m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    UP2dp[i, j] = -1;
                }
            }
            return UP2Find(obstacleGrid, 0, 0);
        }
        int[,] UP2dp;
        public int UP2Find(int[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid[0].Length || y >= grid.Length) { return 0; } // Out of bounds
            else if (x == grid[0].Length - 1 && y == grid.Length - 1) { return 1; } // Reached the destination
            else if (UP2dp[y,x] != -1) { return UP2dp[y,x]; } // Already calculated
            else if (grid[y][x] == 1) { return 0; } // Ran into an obstacle
            return UP2dp[y,x] = UP2Find(grid, x + 1, y) + UP2Find(grid, x, y + 1);
        }
        #region 
        #region Leetcode 78  Subsets
        public IList<IList<int>> Subsets(int[] nums) // Subset doesn't have to be continuous
        {
            int n = nums.Length;
            for (int i = 0; i <= n; ++i)
            { 
                // Try every possible length of our subset
                IList<int> cur = new List<int>();
                dfs(0, ref cur, nums, i);
            }
            return Subset_ans;
        }
        IList<IList<int>> Subset_ans = new List<IList<int>>();
        public void dfs(int index, ref IList<int> path, int[] nums, int k)
        {
            if (path.Count == k)
            {
                // We have reached our target
                Subset_ans.Add(new List<int>(path));
                return;
            }
            for (int i = index; i < nums.Length; ++i)
            {
                path.Add(nums[i]);
                dfs(i + 1, ref path, nums, k);
                path.RemoveAt(path.Count - 1); // Reverse
            }

        }
        #endregion
        #region Leetcode 1446  Consecutive Characters
        public int MaxPower(string s)
        {
            int max = 0, len = 1, index = 0;
            char cur = s[0];
            while (index < s.Length)
            {
                while (index + 1 < s.Length && s[index + 1] == cur)
                {
                    ++index;
                    ++len;
                }
                max = Math.Max(max, len);
                len = 1;
                if (index == s.Length - 1)
                {
                    break;
                }
                cur = s[++index];
            }
            return max;
        }
        #endregion
    }
}
