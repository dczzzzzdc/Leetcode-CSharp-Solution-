using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml.Schema;

namespace Binary_Search
{
    class FenwickTree
    {
        private int[] _nums;
        public FenwickTree(int n)
        {
            _nums = new int[n + 1];
        }
        private int lowbit(int x)
        {
            return x & (-x);
        }
        public void Update(int i,int delta)
        {
            while (i < _nums.Length)
            {
                _nums[i] += delta;
                i += lowbit(i);
            }
        }
        public int Query(int i)
        {
            int sum = 0;
            while (i > 0)
            {
                sum += _nums[i];
                i -= lowbit(i);
            }
            return sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Binary Search Template
        // This function searches for the first occurence of the target
        // The l is inclusive while the r is exclusive
        public int Binary_search_first(int []nums,int target) // Return the index of the target in a sorted array
        // If not found, return -1;
        {
            int l = 0;
            int r = nums.Length;
            int index = -1;
            while (l < r)
            {
                int m = l + (r - l) / 2; 
                int cur = nums[m];
                if(cur >= target) { r = m; } // New range(l,m)
                else { l = m + 1; } // New Range(m+1,r)
                if(cur == target) { index = m; }
            }
            return index;
        }
        
        // This function searches for the last occurence of the target
        public int Binary_search_last(int []nums,int target)
        {
            int l = 0;
            int r = nums.Length;
            int index = -1;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = nums[m];
                if (cur <= target) {l = m+1; } // New range(l,m)
                else { r = m ; } // New Range(m+1,r)
                if (cur == target) { index = m; }
            }
            return index;
        }
        #endregion
        #region Leetcode 35  Search Input Position
        public int SearchInsert(int[] nums, int target) // Basically the same as the template
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = nums[m];
                if (cur == target) { return m; }
                else if (cur >= target) { r = m; } // New range(l,m)
                else { l = m + 1; } // New Range(m+1,r)
            }
            return l;
        }
        #endregion
        #region Leetcode 875 Koko Eating Banana
        public int MinEatingSpeed(int[] piles, int H)
        {
            int l = 1;
            int r = piles.Max() +1; // This is the fastest speed possible
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = 0;
                foreach (int item in piles)
                {
                    cur += (item + m - 1) / m;
                    // This is very important because it will give a Upperbound result
                    // For example, it will give 4 for 7/2, which is the right time instead of 3
                }
                if(cur <= H) { r = m; }// Note that if the time is smaller, then our speed is too big
                else { l = m + 1; }
            }
            return l;
        }
        #endregion
        #region Leetcode 162/852  Mountain Peek Series
        public int PeakIndexInMountainArray(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;
            // The right bound is set to an exclusive nums.Length - 1 and the left is set to an inclusive 1
            // because we can take the first of the last element
            while (left < right)
            {
                int m = left + (right - left) / 2;
                if (nums[m] > nums[m - 1] && nums[m] > nums[m + 1])// We have found the answer
                {
                    return m;
                }
                else if (nums[m] < nums[m + 1])// We are in an ascending order
                {
                    left = m + 1;
                }
                else // We are in an ascending order
                {
                    right = m;
                }
            }
            return left;
        }
        public int FindPeakElement(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1; 
            // This is a little tricky here
            // Although we are in an inclusive-exclusive structure here, we still cannot take nums.Length
            // because calling m+1 might lead to an index out of range
            while (left < right)
            {
                int m = left + (right - left) / 2;
                if (nums[m] < nums[m + 1])
                {
                    left = m + 1;
                }
                else
                {
                    right = m;
                }
            }
            return left;
        }
        #endregion
        #region Leetcode 658  Find k Closest Element
        // We can transfer this question to delete arr.Length - k elements or simply use Binary Search
        public IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            IList<int> ans = new List<int>();
            int left = 0;
            int right = arr.Length - k; // Note that we at least have to leave k space
            // Left and right represents the range of starting
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (x - arr[mid] /*The distance to the start*/ > arr[mid + k] - x /*The distance to the ending*/)
                // This means that we are too far off on the left
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            for (int i = left; i < left + k; i++) // We get k elements starting from left
            {
                ans.Add(arr[i]);
            }
            return ans;
        }
        #endregion
        #region Leetcode 287  Finding Duplicate Numbers
        // This is not really a Binary Search Questions
        // It uses slow and fast pointer which is very very similar to Linked List II
        public int FindDuplicate(int[] nums)
        {
            int slow = nums[0];
            int fast = nums[0];
            //Setting two pointers

            while (true) // We loop until we meet
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
                // Moving the fast pointer two tiles
                if (slow == fast) { break; }
            }
            int start = nums[0];
            while (start != fast)
            {
                fast = nums[fast];
                start = nums[start];
            }
            return start;
        }
        #endregion
        #region Leetcode 436  Find Right Interval
        public int[] FindRightInterval(int[][] intervals)
        {
            int n = intervals.Length;
            if (n == 0) { return null; }
            int[] ans = new int[n];
            (int, int)[] start = new (int, int)[n];
            for (int i = 0; i < n; i++)
            {
                start[i] = (intervals[i][0], i);
            }
            Array.Sort(start);
            for (int i = 0; i < n; i++)
            {
                ans[i] = FRIBinary_Search(start, intervals[i][1]); 
            }
            return ans;

        }
        public int FRIBinary_Search((int,int)[]nums,int target)
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if(nums[m].Item1 >= target)
                {
                    r = m;
                }
                else
                {
                     l= m+1;
                }
            }
            return l == nums.Length /*This means that we have search through the whole array*/ ? -1 : nums[l].Item2; // Note that we are returning the index
            //We are returning the index here
        }
        #endregion
        #region Leetcode 50  Pow(x,n)
        // Basic Theory: Pow(x,n) * Pow(x,n) = Pow(x,2n)
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
        #region Leetcode 378  Kth Smallest Element in a Sorted Matrix

        public int UpperBound(int[] row, int max)
        {
            int left = 0;
            int right = row.Length;
            while (left < right)
            {
                int m = left + (right - left) / 2;
                // Equal or Smaller
                if (row[m] <= max) { left = m + 1; }
                else { right = m; }
            }
            return left;
        }
        public int KthSmallest(int[][] matrix, int k)
        // If we are trying to find the kth element, then we can just find a number that has k numbers that are equal or smaller than it
        {
            int left = matrix[0][0];
            int right = matrix[matrix.Length - 1][matrix[0].Length - 1];
            while (left < right)
            {
                int m = left + (right - left) / 2;
                int total = 0; // The amount of element smaller than m
                foreach (int[] item in matrix)
                {
                    int cur = UpperBound(item, m);
                    if (cur == 0) { break; } // This a prunning
                    // There is no more because the matrix is in an ascending order
                    total += cur;
                }
                // Note that it is smaller here because left is inclusive
                // If total == k but we set it to m+1, then we will permanently lose it
                if (total < k) { left = m + 1; }
                else { right = m; }

            }
            return left;
        }
        #endregion
        #region Leetcode 668  Kth Smallest Number in Multiplication Table
        public int FindKthNumber(int m, int n, int k)
        {
            int l = 1; // The first element in the table is 1
            int r = m * n + 1; // The last element in the table is m*n
            while (l < r)
            {
                int mid = l + (r - l) / 2;
                if(LEX(m,n,mid) >= k)
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }
            // Since we are trying to find the first element that has k element smaller or equal to it
            // Then the number is guranteed to in the table
            return l;
        }
        /// <param name="m">The height of the table</param>
        /// <param name="n">The length of the table</param>
        /// <param name="x">Our target</param>
        /// <returns>The amount of number that is smaller or equal to x in a multiplication table</returns>
        public int LEX(int m, int n,int x)

        {
            int count = 0;
            for (int i = 1; i < m+1; i++)
            {
                count += Math.Min(n, x / i);
                // We want to see how many number that are smaller or equal to x in 1*i,2*i,3*i....i*n
                // Which is converted into x/i : 1,2,3....n
            }
            return count;
        }
        #endregion
        #region Leetcode 786 K-th Smallest Prime Fraction
        public int[] KthSmallestPrimeFraction(int[] A, int k)
        {
            // Theory: Matrix[i][j] = A[i] / A[j]
            // where 0<=i<=n-1 and 1<=j<=n
            // i cannot take the last element and j cannot take the first element
            // The value decreases from left to right because denominator is increasing
            // The value increase from top to down because numerator is increasing
            int n = A.Length;
            double l = 0.0;
            double r = 1.0;
            while (l < r)
            {
                double m = l + (r - l) / 2;
                double max = 0.0;
                int total = 0;
                int p = 0;
                int q = 0;
                for (int i = 0; i < n - 1; i++) // Searching in a virtual 2D matrix
                // During this process, we are find how many factions that are smaller than m 
                {
                    int j = 1;
                    while (j < n && A[i] > m * A[j]) // We want to find the first j that makes A[i]/A[j] < m
                    // Note that the value of the fraction decrease when j increases
                    {
                        ++j;
                    }
                    if (n == j) { break; } // The j does not exist
                    total += n - j; 
                    double cur = Convert.ToDouble(A[i]) / A[j];
                    // We have to keep track of the
                    if (cur > max)
                    {
                        max = cur;
                        q = j;
                        p = i;
                    }

                }
                if (total == k) // We found the answer
                {
                    return new int[2] { A[p], A[q] };
                }
                else if (total > k)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }
            return new int[2];
        }
        #endregion
        #region Leetcode 887  Super Egg Drop
        // This a Dynamic Programming + Binary Search question, which is pretty hard
        public int SuperEggDrop(int K, int N)
        {
            int[][] dp = new int[K + 1][];
            // dp[i][j] represent the minimum attempt need when j levels is left to explore with i eggs
            // Note that means the level that is going to explored instead of the current level we are on
            for (int i = 0; i < K + 1; ++i)
            {
                dp[i] = new int[N + 1];
                Array.Fill(dp[i], int.MaxValue);
            }
            for (int i = 0; i <= N; ++i)
            { // When are no eggs left, then we can stop trying
                dp[0][i] = 0;
                // When we only have one egg, then we need i attempts in the worst case
                dp[1][i] = i;
            }
            for (int i = 0; i <= K; ++i)
            {
                // When we are on the first floor
                dp[i][0] = 0;
            }
            for (int k = 2; k <= K; k++)
            {
                for (int n = 1; n <= N ; n++)
                {
                    // We are using Binary Search to find the valley
                    int l = 1;
                    int r = n;
                    // Why can we use Binary Search?
                    // When there are more levels left, we definitely need more attempts
                    // When dp[k-1][m-1] == dp[k][n-m], we are able to find the minimum amount of attempts
                    while (l <= r)
                    {
                        int m = (r - l) / 2 + l;
                        int x = dp[k - 1][m - 1]; // It is broken
                        int y = dp[k][n - m]; // Not Broken
                        if (x > y)
                        {
                            r = m - 1;
                        }
                        else
                        {
                            l = m + 1;
                        }
                    }
                    // Brute Force that reached Time Limit O(N3)
                    // for (int f = 0; f <= n; f++)  We can choose to throw from floor 1 to n
                    // {
                    //  dp[k][n] = Math.Min(dp[k][n], Math.Max(dp[k - 1][f - 1], dp[k][n - f]) + 1);
                    // }
                }
            }
            return dp[K][N];
        }
        #endregion
        #region Leetcode 1337  The K Weakest Row in a Matrix
        public int[] KWeakestRows(int[][] mat, int k)
        {
            (int, int)[] rank = new (int, int)[mat.Length];
            for (int i = 0; i < mat.Length; ++i)
            {
                rank[i] = (Search(mat[i]), i);
            }
            Array.Sort(rank);
            int[] ans = new int[k];
            for (int i = 0; i < k; ++i)
            {
                ans[i] = rank[i].Item2;
            }
            return ans;
        }
        public int Search(int[] row)
        {
            int l = 0;
            int r = row.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                if (row[m] == 1)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }
            return l + 1;
        }
        #endregion
        #region Leetcode 74 Search a 2D Matrix
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
        public bool SearchMatrix(int[,] matrix, int target)
        {
            int n = matrix.Length;
            int m = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                int l = 0; int r = m;
                while (l < r)
                {
                    int mid = l + (r - l) / 2;
                    if(matrix[i,mid]> target) { r = mid; }
                    else if (matrix[i,mid] < target) { l = mid + 1; }
                    else { return true; }
                }
            }
            return false;
        }
        public bool SearchMatrix_LinearScan(int[,] matrix, int target)
        {
            // Starting Searching from top right corner because we will only move left or down
            int i = 0;
            int j = matrix.GetLength(1) - 1;

            while (i < matrix.GetLength(0) && j >= 0)
            {
                if (target > matrix[i, j])
                // We do not have to search matrix[i][1-j] because all the numbers on the left is smaller
                // Therefore, we can move down by one row while keeping j the same
                {
                    i++;
                }
                // We keep moving down until we meet a larger numbers
                else if (target < matrix[i, j])
                {
                    j--;
                }
                else
                {
                    return true;
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
        #region Leetcode 34  Find First and Last Position of Element in Sorted Array
        public int[] SearchRange(int[] nums, int target)
        {
            int[] ans = new int[2];
            ans[0] = FindFirst(nums, target);
            ans[1] = FindLast(nums, target);
            return ans;
        }
        public int FindFirst(int [] nums,int target)
        {
            int index = -1;
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = nums[m];
                if(cur >= target)
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
                if(cur == target) { index = m; }
            }
            return index;
        }
        public int FindLast(int []nums, int target)
        {
            int index = -1;
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2;
                int cur = nums[m];
                if (cur <= target)
                {
                    l = m+1;
                }
                else
                {
                    r = m;
                }
                if (cur == target) { index = m; }
            }
            return index;
        }
        #endregion
        #region Leetcode 1482  Minimum Number of Days to Make m Bouquets
        public int MinDays(int[] bloomDay, int n, int k)
        {
            if(bloomDay.Length < n * k) { return -1; }
            int l = int.MaxValue; int r = 0;
            foreach (var item in bloomDay)
            {
                l = Math.Min(l, item);
                r = Math.Max(r, item);
            }
            while (l < r)
            {
                int m = (r - l) / 2 + l;
                if (CollectFlower(m, bloomDay, k)>=n)
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }
            return l;
        }
        /// <summary>
        /// This finds how many bouquets that can be made in given days
        /// </summary>
        /// <param name="givenDays"></param>
        /// <param name="bloomDay">The Array containing the blooming days</param>
        /// <param name="k">The amount of flowers </param>
        /// <returns>Returns the amount of bouquets</returns>
        public int CollectFlower(int givenDays,int[]bloomDay,int k)
        {
            int count = 0; int flower = 0;
            foreach (int day in bloomDay)
            {
                if(day > givenDays) // This flower has not bloomed yet
                {
                    flower = 0; // Since we have to use adjacent flower, we have to clear the flower count
                    continue;
                }
                else if(++flower == k) // We successfully made a bouquet
                {
                    flower = 0;
                    count++;
                }
            }
            return count;
        }
        #endregion
    }

}
