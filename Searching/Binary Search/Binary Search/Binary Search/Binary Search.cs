using System;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Xml.Schema;

namespace Binary_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] test =  { 1, 2, 3, 4, 5, 6,7};
            Console.WriteLine(FindPeakElement(test));
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
    }

}
