using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Xml.Schema;

namespace Binary_Search
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Binary Search Template

        // The l is inclusive while the r is exclusive
        public static int Binary_search(int []nums,int target) // Return the index of the target in a sorted array
        // If not found, return -1;
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2; // The middle of the range (l,r)
                int cur = nums[m];
                if(cur == target) { return m; }
                else if(cur >= target) { r = m; } // New range(l,m)
                else { l = m + 1; } // New Range(m+1,r)
            }
            return -1;
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
    }

}
