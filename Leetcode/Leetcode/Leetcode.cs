﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json.Serialization;

namespace Leetcode
{
    class Prefix_Sum
    {
        private readonly int[] prefix;

        public Prefix_Sum(int[] data)
        {
            int n = data.Length;
            prefix = new int[n+1];

            for (int i = 0; i < n; i++)
            {
                prefix[i] = prefix[i - 1] + data[i];
            }
        }
        public int Query(int i, int j)
        {
            return prefix[j] - prefix[i - 1];
        }
    }
    class Union_Find
    {
        private readonly int[] parent;
        private readonly int[] size;
        private readonly int N;
        public Union_Find(int n)
        {
            this.N = n;
            parent = new int[N];
            size = new int[N];

            for (int i = 0; i < N; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }
        
        /// <summary>
        /// Find the parent of target while doing path compression
        /// </summary>
        public int Find(int target)
        {
            if (target == parent[target]) { return target; }
            return parent[target] = Find(parent[target]); // Path compression
        }

        /// <summary>
        /// Union x and y into the same group
        /// </summary>
        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                parent[rootY] = parent[rootX];
                size[rootX] += size[rootX];
            }

        }

        public int GroupCount()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                if(parent[i] == i)
                {
                    ++count;
                }
            }
            return count;
        }
    }
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
    class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val =0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    class Solutions
    {
        #region Templates
        #region Binary Search
        /// <summary>
        /// The Binary Search template
        /// </summary>
        /// <param name="nums">The given sorted array</param>
        /// <param name="target">The target we are trying to find in the array</param>
        /// <returns>Returns the index of the target in the array. Returns -1 if not found</returns>
        public int BinarySearch(int[] nums, int target)
        {
            int l = 0, r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (nums[m] == target) { return m; } // Found the target
                else if (nums[m] > target) { r = m; }
                else { l = m + 1; }
            }
            return l;
        }
        #endregion
        #endregion
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
                if (n - i < len / 2) // There is no way to achieve greater length
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
            if (x < 0 || (x != 0 && x % 10 == 0))
            {
                return false;
            }
            int last_part = 0;

            while (x > last_part)
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
                if (cur > 0)
                {
                    // No way to Find two positive numbers to make a negative sum
                    // Since the array is sorted, the numbers after it are also positive so the loop can break
                    break;
                }
                if (i > 0 && cur == nums[i - 1]) // Avoid the same value
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
            if (l1 == null)
            {
                return l2;
            }
            else if (l2 == null)
            {
                return l1;
            }
            ListNode dummy = new ListNode(0);
            ListNode head = dummy;
            while (l1 != null && l2 != null)
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
            if (n == 0)
            {
                return null;
            }
            while (interval < n)
            {
                for (int i = 0; i < n - interval; i *= 2)
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
                if (i + nums[i] >= last)
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
            while (tail >= 0 && Char.IsWhiteSpace(s[tail]))
            {
                --tail;
            }
            while (tail >= 0 && !Char.IsWhiteSpace(s[tail]))
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
            UPdp = new int[y, x];
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
            else if (UPdp[y, x] != -1) { return UPdp[y, x]; }
            else
            {
                return UPdp[y, x] = UPFind(x - 1, y) + UPFind(x, y - 1);
            }
        }
        #endregion
        #region Leetcode 63  Unique Path II
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int n = obstacleGrid.Length;
            if (obstacleGrid == null || n == 0) { return 0; }
            int m = obstacleGrid[0].Length;
            if (obstacleGrid[n - 1][m - 1] == 1) { return 0; } // Impossible to reach the destination
            UP2dp = new int[n, m];
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
            else if (UP2dp[y, x] != -1) { return UP2dp[y, x]; } // Already calculated
            else if (grid[y][x] == 1) { return 0; } // Ran into an obstacle
            return UP2dp[y, x] = UP2Find(grid, x + 1, y) + UP2Find(grid, x, y + 1);
        }
        #endregion
        #region Leetcode 64  Minimum Path
        public int MinPathSum(int[][] grid)
        {
            int y = grid.Length;
            if (y == 0) { return 0; }
            int x = grid[0].Length;
            int[,] dp = new int[y, x];

            // Initiallize the boundary
            // Suppose grid is like[1, 1, 1, 1]
            // The minimum sum is simply an accumulation of previous points
            // The result is [1, 2, 3, 4]
            dp[0, 0] = grid[0][0];
            for (int i = 1; i < x; i++)
            {
                dp[0, i] = dp[0, i - 1] + grid[0][i];
            }
            for (int i = 1; i < y; i++)
            {
                dp[i, 0] = dp[i - 1, 0] + grid[i][0];
            }

            for (int i = 1; i < y; i++)
            {
                for (int j = 1; j < x; j++)
                {
                    dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
                }
            }
            return dp[y - 1, x - 1];
        }
        #endregion
        #region Leetcode 66  Plus One
        public int[] PlusOne(int[] digits)
        {
            int n = digits.Length;

            for (int i = n - 1; i >= 0; i--)
            {
                if (digits[i] != 9)
                {
                    digits[i]++;
                    return digits;
                }
                else
                {
                    digits[i] = 0;
                }
            }

            // The entire array is consisted of 9
            int[] ans = new int[n + 1];
            ans[0] = 1;
            return ans;
        }
        #endregion
        #region Leetcode 67  Add Binary
        public string AddBinary(string a, string b)
        {
            StringBuilder res = new StringBuilder("");
            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;
            while (i >= 0 || j >= 0)
            {
                int sum = carry;
                if (i >= 0) { sum += a[i--] - '0'; }
                if (j >= 0) { sum += b[j--] - '0'; }

                res.Insert(0, sum % 2);
                carry = sum / 2;

            }
            if (carry != 0)
            {
                res.Insert(0, 1);
            }

            return res.ToString();
        }
        #endregion
        #region Leetcode 69  Sqrt(x)
        public int MySqrt(int x)
        {
            if (x <= 1) return x;
            int low = 0, high = x;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                // If mid*mid <= x and (mid+1) * (mid+1) > x, return the current mid. 
                // This avoids the overflow
                if (mid <= x / mid && (mid + 1) > x / (mid + 1)) { return mid; }
                else if (mid < x / mid) { low = mid + 1; }
                else { high = mid; }
            }
            return low;
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
        #region Leetcode 72  Edit Distance
        public int MinDistance(string w1, string w2)
        {
            word1 = w1;
            word2 = w2;
            int l1 = w1.Length;
            int l2 = w2.Length;
            MEDdp = new int[l1 + 1, l2 + 1];
            for (int i = 0; i < l1 + 1; i++)
            {
                for (int j = 0; j < l2 + 1; j++)
                {
                    MEDdp[i, j] = 1;
                }
            }
            return MEDfind(l1, l2);

        }
        int[,] MEDdp;
        string word1;
        string word2;
        public int MEDfind(int i1, int i2)
        {
            if (i1 == 0) // Finished editing word 1
            {
                return i2;
            }
            else if (i2 == 0) // Finished editing word 2
            {
                return i1;
            }
            else if (MEDdp[i1, i2] != -1) // Already calculated this circumstance
            {
                return MEDdp[i1, i2];
            }
            int result = 0;
            if (word1[i1 - 1] == word2[i2 - 1]) // The current two characters are the same so we can skip it
            {
                result = MEDfind(i1 - 1, i2 - 1);
            }
            else
            {
                result = Math.Min(Math.Min(MEDfind(i1 - 1, i2 - 1), MEDfind(i1 - 1, i2)), MEDfind(i1, i2 - 1)) + 1;
                // Select the best operation from three
            }
            return MEDdp[i1, i2] = result;
        }
        #endregion
        #region Leetcode 74  Search a 2D Matrix
        public bool SearchMatrix(int[][] matrix, int target)
        {
            if (matrix.Length == 0 || matrix == null) { return false; }
            int n = matrix[0].Length;
            if (n == 0)
            {
                return false;
            }
            for (int i = 0; i < matrix.Length; ++i)
            {
                if (matrix[i][0] <= target && matrix[i][n - 1] >= target)
                {
                    if (SMsearch(matrix[i], target))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool SMsearch(int[] nums, int target)
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (nums[m] == target)
                {
                    return true;
                }
                else if (nums[m] > target)
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }
            return false;
        }
        #endregion
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
        #region Leetcode 79  Word Search
        public bool Exist(char[][] board, string word)
        {
            int n = board.Length;
            if (n == 0) { return false; }
            int m = board[0].Length;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if (board[i][j] == word[0]) // It can be used as a starting point
                    {
                        if (WSsearch(j, i, 0, word, ref board))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool WSsearch(int x, int y, int index, string search, ref char[][] board)
        {
            if (y < 0 || y >= board.Length || x < 0 || x >= board[0].Length || board[y][x] != search[index])
            // Out of bounds or mismatch
            {
                return false;
            }
            else if (index == search.Length - 1) { return true; } // Went through the entire word
            else
            {
                char cur = board[y][x];
                board[y][x] = '$'; // Visited
                bool result = WSsearch(x + 1, y, index + 1, search, ref board) ||
                    WSsearch(x - 1, y, index + 1, search, ref board) ||
                    WSsearch(x, y - 1, index + 1, search, ref board) ||
                    WSsearch(x, y + 1, index + 1, search, ref board); // Expand into four directions
                board[y][x] = cur;
                return result;
            }
        }
        #endregion
        #region Leetcode 84  Largest Rectangle in Histogram
        public int LargestRectangleArea(int[] heights)
        {
            int max = 0;
            Stack<int> s = new Stack<int>(); // Store the index
            s.Push(-1); // Put a buffer
            
            int n = heights.Length;
            for (int i = 0; i < n; i++) // Try to put every element in the stack
            {
                while (s.Peek() != -1 && heights[s.Peek()] >= heights[i])
                {
                    max = Math.Max(max, heights[s.Pop()] * (i - s.Peek()- 1));
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
        #region Leetcode 86  Partition List
        public ListNode Partition(ListNode head, int x)
        {
            ListNode dummy1 = new ListNode(0), dummy2 = new ListNode(0);
            ListNode l1 = dummy1, l2 = dummy2;

            while(head != null)
            {
                if(head.val < x)
                {
                    l1.next = head;
                    l1 = head;
                }
                else
                {
                    l2.next = head;
                    l2 = head;
                }
                head = head.next;
            }

            l2.next = null;
            l1.next = dummy2.next;

            return dummy1.next;
        }
        #endregion
        #region Leetcode 91  Decode Ways
        public int NumDecodings(string s)
        {
            int n = s.Length;
            if (n == 0 || s == null || s[0] == '0') { return 0; }
            else if (n == 1) { return 1; }

            int[] dp = new int[n];
            dp[0] = 1;
            for (int i = 1; i < n; i++)
            {
                bool r1 = ValidToFormNumber(s[i]);
                bool r2 = ValidToFormNumber(s[i - 1], s[i]);
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
        public bool ValidToFormNumber(char a)
        {
            return a != '0';
        }
        public bool ValidToFormNumber(char a, char b)
        {
            int value = Convert.ToInt32(a - '0') * 10 + Convert.ToInt32(b - '0');
            return value >= 10 && value <= 26;
        }
        #endregion
        #region Leetcode 92  Reverse Linked List II
        public static ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if(head == null) { return null; }
            ListNode dummy = new ListNode(0);
            dummy.next = head;

            ListNode pre = dummy;
            for (int i = 0; i < m -1 ; i++) { pre = pre.next; }

            ListNode start = pre.next;
            ListNode next = pre.next.next;

            for (int i = 0; i < n-m; i++)
            {
                start.next = next.next;

                next.next = pre.next;
                pre.next = next;

                next = start.next;
            }
            return dummy.next;
        }
        #endregion
        #region Leetcode 101  Symmetric Tree
        public bool IsSymmetric(TreeNode root)
        {
            return root == null || IsSymmetricDfs(root.left, root.right);
        }
        private bool IsSymmetricDfs(TreeNode left, TreeNode right)
        {
            if(left == null || right == null)
            {
                return left == right;
            }
            else if (left.val != right.val)
            {
                return false;
            }
            return IsSymmetricDfs(left.left, right.right) && IsSymmetricDfs(left.right, right.left);

        }
        #endregion
        #region Leetcode 102  Binary Tree Level Order Traversal
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            
            IList<IList<int>> ans = new List<IList<int>>();
            if (root == null)
            {
                return ans;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while(q.Count != 0)
            {
                int count = 0;
                List<int> level = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    TreeNode cur = q.Dequeue();
                    level.Add(cur.val);
                    if(cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if(cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
                ans.Add(level);
            }
            return ans;
        }
        #endregion
        #region Leetcode 103  Binary Tree Zigzag Level Order Traversal
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            IList<IList<int>> ans = new List<IList<int>>();
            bool reverse = false;

            while (q.Count != 0)
            {
                int count = q.Count;
                List<int> cur = new List<int>();

                for (int i = 0; i < count; i++)
                {
                    TreeNode node = q.Dequeue();
                    cur.Add(node.val);
                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }

                if (reverse)
                {
                    cur.Reverse();
                }
                ans.Add(cur);
                reverse = !reverse;
            }

            return ans;
        }
        #endregion
        #region Leetcode 206  Reverse Linked List
        public ListNode ReverseList(ListNode head)
        {
            ListNode nHead = null;
            while(head != null)
            {
                ListNode hNext = head.next;

                head.next = nHead;
                nHead = head;

                head = hNext; // head = head.next
            }
            return nHead;
        }
        #endregion
        #region Leetcode 445  Add Two Numbers II
        // Original function name: AddTwoNumbers, changed to avoid clash with Leetcode 2
        public ListNode AddTwoNumbersII(ListNode l1, ListNode l2)
        {
            Stack<int> s1 = new Stack<int>();
            Stack<int> s2 = new Stack<int>();

            while (l1 != null)
            {
                s1.Push(l1.val);
                l1 = l1.next;
            }
            while (l2 != null)
            {
                s2.Push(l2.val);
                l2 = l2.next;
            }
            ListNode dummy = null;
            int carry = 0;
            while (s1.Count != 0 || s2.Count != 0 || carry != 0)
            {
                int sum = carry;
                if (s1.Count != 0) { sum += s1.Pop(); }
                if (s2.Count != 0) { sum += s2.Pop(); }


                ListNode newNode = new ListNode(sum % 10);
                newNode.next = dummy;
                dummy = newNode;

                carry = sum / 10;
            }
            return dummy;

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
