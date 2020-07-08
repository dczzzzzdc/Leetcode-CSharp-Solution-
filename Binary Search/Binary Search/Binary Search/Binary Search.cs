using System;

namespace Binary_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Binary Search Template
        // The l is inclusive while the r is exclusive
        public int Binary_search(int []nums,int target) // Return the index of the target in a sorted array
        // If not found, return -1;
        {
            int l = 0;
            int r = nums.Length;
            while (l < r)
            {
                int m = l + (r - l) / 2; // The middle of the range (l,r)
                int cur = nums[m];
                if(nums[m] == target) { return m; }
                else if(nums[m] >= target) { r = m; } // New range(l,m)
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
    }
}
