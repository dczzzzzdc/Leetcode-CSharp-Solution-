using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Leetcode
{
    class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;
        public Node random;
        public Node() { }

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }

    }
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
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2, int q_Index = 1)
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
                Subsetdfs(0, ref cur, nums, i);
            }
            return Subset_ans;
        }
        IList<IList<int>> Subset_ans = new List<IList<int>>();
        public void Subsetdfs(int index, ref IList<int> path, int[] nums, int k)
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
                Subsetdfs(i + 1, ref path, nums, k);
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
        #region Leetcode 104  Maximum Depth of Binary Tree
        public int MaxDepth(TreeNode root)
        {
            if (root == null) { return 0; }
            int level = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                int count = q.Count;
                ++level;
                for (int i = 0; i < count; ++i)
                {
                    TreeNode cur = q.Dequeue();
                    if (cur.left != null)
                    {
                        q.Enqueue(cur.left);
                    }
                    if (cur.right != null)
                    {
                        q.Enqueue(cur.right);
                    }
                }
            }
            return level;
        }
        #endregion
        #region Leetcode 107  Binary Tree Level Order Traversal II
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            if (root == null)
            {
                return new List<IList<int>>();
            }
            IList<IList<int>> ans = new List<IList<int>>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
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
                ans.Insert(0, cur);
            }
            return ans;
        }
        #endregion
        #region Leetcode 112  Path Sum 
        public bool HasPathSum(TreeNode root, int sum)
        {
            return HPSdfs(root, 0, sum);
        }
        private bool HPSdfs(TreeNode cur, int sum, int target)
        {
            if (cur == null)
            {
                return false;
            }
            sum += cur.val;
            if (sum == target && cur.left == null&& cur.right == null) 
            // Reached the target sum and the current node is a leave
            {
                return true;
            }
            return HPSdfs(cur.left, sum, target) || HPSdfs(cur.right, sum, target);
        }
        #endregion
        #region Leetcode 113 Path Sum II
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            PSIIdfs(root, new List<int>(), 0, sum);
            return PSIIans;
        }
        IList<IList<int>> PSIIans = new List<IList<int>>();
        private void PSIIdfs(TreeNode cur, List<int> path, int sum, int target)
        {
            if (cur == null)
            {
                return;
            }
            sum += cur.val;
            path.Add(cur.val);
            if (sum == target && (cur.left == null && cur.right ==null))
            {
                PSIIans.Add(new List<int>(path));
            }
            else
            {
                PSIIdfs(cur.left, path, sum, target);
                PSIIdfs(cur.right, path, sum, target);
            }
            path.RemoveAt(path.Count - 1);
        }
        #endregion
        #region Leetcode 115  Distinct Subsequence
        public int NumDistinct(string s, string t)
        {
            int ls = s.Length, lt = t.Length;

            int[,] dp = new int[lt + 1, ls + 1];
            //dp[i,j] = the amount of distinct subsequence in s[0:j - 1] that is the same as t[0:i - 1]

            for (int i = 0; i < ls + 1; i++)
            {
                dp[0,i] = 1; // When the target is empty, there is one subsequence, which is empty
            }
            for (int i = 1; i < lt + 1; i++)
            {
                for (int j = 1; j < ls + 1; j++)
                {
                    dp[i, j] = dp[i, j - 1]; // Skip these chars
                    if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] += dp[i - 1,j - 1]; // Match these two chars
                    }
                }
            }
            return dp[lt, ls];
        }
        #endregion
        #region Leetcode 116  Populating Next Right Pointers in Each Node
        public Node Connect(Node root)
        {
            if (root == null)
            {
                return null;
            }
            ConnectTwoNodes(root.left, root.right);
            return root;
        }
        public void ConnectTwoNodes(Node root1, Node root2)
        {
            if (root2 == null || root1 == null)
            {
                return;
            }

            root1.next = root2;

            ConnectTwoNodes(root1.left, root1.right);
            ConnectTwoNodes(root2.left, root2.right);

            ConnectTwoNodes(root1.right, root2.left);
        }
        #endregion
        #region Leetcode 119  Pascal's Triangle II
        public IList<int> GetRow(int rowIndex)
        {
            IList<int> level = new List<int>();
             
            if (rowIndex == 0) // First row
            {
                level.Add(1);
                return level;
            }

            level.Add(1);
            level.Add(1); // Second row
            for (int i = 0; i < rowIndex - 1; ++i)
            {
                level = update(level);
            }
            return level;
        }
        public IList<int> update(IList<int> level)
        {
            IList<int> cur = new List<int>();
            cur.Add(1);
            for (int i = 0; i < level.Count - 1; ++i)
            {
                cur.Add(level[i] + level[i + 1]);
            }
            cur.Add(1);
            return cur;
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
                    // triangle[i][j]: the minimum path sum from the bottom column to (i,j)
                }
            }

            return triangle[0][0];
        }
        #endregion
        #region Leetcode 121  Best Time to Buy and Sell Stock
        public int MaxProfit(int[] prices)
        {
            int minBuyIn = int.MaxValue;
            int maxProfit = 0;
            int n = prices.Length;
            if (n == 0 || n == 1) { return 0; } // Unable to make a deal
            for (int i = 0; i < n; ++i)
            {
                // Keep updating the lowest price and max profit
                if (prices[i] < minBuyIn)
                {
                    minBuyIn = prices[i];
                }
                else if(prices[i] - minBuyIn > maxProfit) // Use else if here because buying and selling cannot be on the same day
                {
                    maxProfit = prices[i] - minBuyIn;
                }
            }
            return maxProfit;
        }
        #endregion
        #region Leetcode 122  Best Time to Buy and Sell Stock II 
        public int MaxProfitII(int[] prices, int qIndex = 2)
        {
            int n = prices.Length;
            if (n <= 1) { return 0; }
            int buy = 0; int sell = 0; int profit = 0;
            // Goal here is to as much positive trades as possible
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
        #region Leetcode 123  Best Time to Buy and Sell Stock III
        public int MaxProfit(int[] prices, int qIndex = 3)
        {
            int n = prices.Length;
            if (n == 0) { return 0; }
            int k = 2;
            int[,] dp = new int[k + 1, n];
            for (int i = 1; i <= k; i++)
            {
                int maxdiff = -prices[0];
                for (int j = 1; j < n; j++)
                {
                    dp[i, j] = Math.Max(dp[i, j - 1], maxdiff + +prices[j]);
                    maxdiff = Math.Max(maxdiff, dp[i - 1, j] - prices[j]);
                }
            }
            return dp[k, n - 1];

        }
        #endregion
        #region Leetcode 124  Binary Tree Maximum Path Sum
        public int MaxPathSum(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            int ans = int.MinValue;
            MPShelper(root, ref ans);
            return ans;
        }
        /// <summary>
        /// The helper function of Max Path Sum
        /// </summary>
        /// <param name="root">The current root</param>
        /// <param name="ans">An ref integar that represent the possible maximum subtree sum</param>
        /// <returns>Returns the path sum of the branch(left or right) with the largest path sum</returns>
        public int MPShelper(TreeNode root, ref int ans)
        {
            if (root == null)
            {
                return 0;
            }
            int left_sum = Math.Max(0, MPShelper(root.left, ref ans));
            int right_sum = Math.Max(0, MPShelper(root.right, ref ans));

            ans = Math.Max(ans, left_sum + right_sum + root.val);
            return Math.Max(left_sum, right_sum) + root.val;

        }
        #endregion
        #region Leetcode 125  Valid Panlindrome
        public bool IsPalindrome(string s)
        {
            int n = s.Length;
            int l = 0;
            int r = n - 1;
            while (l < r)
            {
                // Skip white spaces
                while (l < r && !Char.IsLetterOrDigit(s[l]))
                {
                    ++l;
                }
                while (l < r && !Char.IsLetterOrDigit(s[r]))
                {
                    --r;
                }

                if (Char.ToLower(s[l++]) != Char.ToLower(s[r--]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Leetcode 129  Sum Root to Leaf Numbers
        public int SumNumbers(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            dfs(new StringBuilder(), root);
            return SNans;
        }
        int SNans = 0;
        private void dfs(StringBuilder path, TreeNode cur)
        {
            path.Append(cur.val);
            if (cur.left == null && cur.right == null)
            {
                SNans += Convert.ToInt32(path.ToString());
            }
            else
            {
                if (cur.left != null)
                {
                    dfs(path, cur.left);
                }
                if (cur.right != null)
                {
                    dfs(path, cur.right);
                }
            }

            path.Remove(path.Length - 1, 1);
        }
        #endregion
        #region Leetcode 130  Surrounded Regions
        public void Solve(char[][] board)
        {
            int m = board.Length;
            if (m == 0)
            {
                return;
            }
            int n = board[0].Length;
            for (int i = 0; i < m; i++)
            {
                SRdfs(ref board, n - 1, i);
                SRdfs(ref board, 0, i);
            }
            for (int i = 0; i < n; i++)
            {
                SRdfs(ref board, i, 0);
                SRdfs(ref board, i, m - 1);
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i][j] == 'O')
                    {
                        board[i][j] = 'X';
                    }
                    else if (board[i][j] == 'S')
                    {
                        board[i][j] = 'O';
                    }
                }
            }
        }
        private void SRdfs(ref char[][] board, int x, int y)
        {
            if (x < 0 || y < 0 || x >= board[0].Length || y >= board.Length || board[y][x] != 'O')
            {
                return;
            }

            board[y][x] = 'S';
            SRdfs(ref board, x + 1, y);
            SRdfs(ref board, x - 1, y);
            SRdfs(ref board, x, y + 1);
            SRdfs(ref board, x, y - 1);
        }
        #endregion
        #region Leetcode 134  Gas Station
        // This is actually not the most optimized solution
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int n = gas.Length;
            for (int i = 0; i < n; ++i) // Try every starting position
            {
                int total = 0, count = 0, j = i;
                while (count < n)
                {
                    total += gas[j % n] - cost[j % n]; // Use the mod here because j could be larger than n
                    if (total < 0) // Run out of gases
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
        #endregion
        #region Leetcode 136  Single Number
        public int SingleNumber(int[] nums)
        {
            int ans = 0;
            foreach (int num in nums)
            {
                ans ^= num;
            }
            return ans;
        }
        #endregion
        #region Leetcode 137  Single Number II
        public int SingleNumber(int[] nums,int qIndex = 2)
        {
            int res = 0;
            for (int i = 0; i < 32; i++)
            {
                int sum = 0;
                for (int j = 0; j < nums.Length; j++)
                {
                    if (((nums[j] >> i) & 1) == 1)
                    {
                        ++sum;
                    }
                }
                sum %= 3;
                // A set bit must occur 3n times or it is also a bit of the single number
                if (sum == 1)
                {
                    res += 1 << i;
                }
            }
            return res;
        }
        #endregion
        #region Leetcode 138  Copy List with Random Pointer
        public Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return null;
            }

            Dictionary<Node, int> position = new Dictionary<Node, int>();
            // Key: The Node  Value: The original index of the node
            Node temp = head;
            List<Node> nodemap = new List<Node>();
            // The list

            int index = 0;
            while (temp != null) // Traverse
            {
                nodemap.Add(new Node(temp.val));
                position.Add(temp, index);
                temp = temp.next; 
                ++index;
            }

            int length = index;
            temp = head;
            index = 0;
            while (temp != null)
            {
                // nodemap[index] is the current node
                if (index < length - 1)
                {
                    nodemap[index].next = nodemap[index + 1];
                    // Connect the normal pointers 
                }
                if (temp.random != null) // If it do have a random pointer
                {
                    nodemap[index].random = nodemap[position[temp.random]];
                }

                temp = temp.next;
                ++index;
            }
            return nodemap[0];
        }
        #endregion
        #region Leetcode 139  Word Break
        public bool WordBreak(string s, IList<string> wordDict)
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
        #endregion
        #region Leetcode 141  Linked List Cycle
        public bool HasCycle(ListNode head)
        {
            if (head == null)
            {
                return false;
            }
            ListNode fast = head, slow = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;

                if (fast == slow)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Leetcode 142  Linked List Cycle II
        /*
        Assume the distance from head to the start of the loop is x1
        the distance from the start of the loop to the point fast and slow meet is x2
        the distance from the point fast and slow meet to the start of the loop is x3
        What is the distance fast moved? What is the distance slow moved? And their relationship?

        x1 + x2 + x3 + x2
        x1 + x2
        x1 + x2 + x3 + x2 = 2 (x1 + x2)

        Thus x1 = x3
        */

        public ListNode DetectCycle(ListNode head)
        {
            if(head == null || head.next == null)
            {
                return null;
            }

            bool hasCycle = false;
            ListNode fast = head, slow = head;
            while(fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                if(fast.next == null)
                {
                    return null;
                }
                fast = fast.next.next;
                if(fast == slow)
                {
                    hasCycle = true;
                    break;
                }
            }

            if (!hasCycle)
            {
                return null;
            }

            fast = head;
            while(fast != slow)
            {
                fast = fast.next;
                slow = slow.next;
            }
            return fast;
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
        #region Leetcode 310  Minimum Height Tree
        // Distance between two nodes is the amount of edges that connect the two nodes
        // Height of a tree can be defined as the maximum distance between its root and one of its leaves

        // This question can be converted to finding centroids. Centroids are nodes that are closest to leaf nodes
        // Not only that, there are atmost two centroids in a tree

        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            IList<int> ans = new List<int>(); 
            if(n <= 2)
            {
                for (int i = 0; i < n; i++)
                {
                    ans.Add(i);
                }
                return ans;
            }
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach(int[] e in edges)
            {
                int u = e[0], v = e[1];

                graph[u].Add(v);
                graph[v].Add(u);
            }


            List<int> leaves = new List<int>();
            for (int i = 0; i < n; i++) // Initiallize the first layer of leaves
            {
                if(graph[i].Count == 1)
                {
                    leaves.Add(i);
                }
            }

            int remaining = n;
            while (remaining > 2)
            {
                remaining -= leaves.Count;
                List<int> newLeaves = new List<int>();
                
                // Find the inner layer of leaves


                foreach(int leaf in leaves)
                {
                    foreach(int next in graph[leaf])
                    {
                        graph[next].Remove(leaf);
                        if(graph[next].Count == 1)
                        {
                            newLeaves.Add(next);
                        }
                    }
                }
                leaves = newLeaves;
            }
            return leaves;
        }
        #endregion
        #region Leetcode 445  Add Two Numbers II
        // Original function name: AddTwoNumbers, changed to avoid clash with Leetcode 2
        public ListNode AddTwoNumbersII(ListNode l1, ListNode l2, int qIndex = 2)
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
        #region Leetcode 593  Valid Square
        public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            IDictionary<int, int> distMap = new Dictionary<int, int>();
            int[][] points = new int[4][];
            points[0] = p1;
            points[1] = p2;
            points[2] = p3;
            points[3] = p4;

            for (int i = 0; i < 4; ++i)
            {
                for (int j = i + 1; j < 4; ++j)
                {
                    int[] p = points[i];
                    int[] q = points[j];
                    int dist = CalculateDistSquare(p, q);
                    if (distMap.ContainsKey(dist))
                    {
                        distMap.Add(dist, 0);
                    }
                    distMap[dist]++;
                }
            }

            if (distMap.Count != 2)
                return false;

            foreach (var kvp in distMap)
            {
                if (kvp.Value!= 2 && kvp.Value!= 4)
                    return false;
            }

            return true;
        }

        private int CalculateDistSquare(int[] p1, int[] p2)
        {
            return (p1[0] - p2[0]) * (p1[0] - p2[0]) + (p1[1] - p2[1]) * (p1[1] - p2[1]);
        }
        #endregion
        #region Leetcode 938  Range Sum of BST
        public int RangeSumBST(TreeNode root, int low, int high)
        {
            if (root == null)
            {
                return 0;
            }
            int ans = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count != 0)
            {
                TreeNode cur = q.Dequeue();
                if (cur.val <= high && cur.val >= low)
                {
                    ans += cur.val;
                }

                if (cur.left != null)
                {
                    q.Enqueue(cur.left);
                }
                if (cur.right != null)
                {
                    q.Enqueue(cur.right);
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 1026  Maximum Difference Between Node and Ancestor
        public int MaxAncestorDiff(TreeNode root)
        {
            if (root == null)
            {
                return MADans;
            }

            MADdfs(root.left, root.val, root.val);
            MADdfs(root.right, root.val, root.val);

            return MADans;
        }
        int MADans = 0;
        private void MADdfs(TreeNode node, int prev_min, int prev_max)
        {
            if (node == null) { return; }
            MADans = Math.Max(MADans, Math.Max(Math.Abs(prev_min - node.val), Math.Abs(prev_max - node.val)));
            int new_min = Math.Min(node.val, prev_min), new_max = Math.Max(node.val, prev_max);

            MADdfs(node.left, new_min, new_max);
            MADdfs(node.right, new_min, new_max);
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
