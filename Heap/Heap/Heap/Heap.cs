using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Heap
{
    class Heap
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 215  Kth Largest Element in an Array
        // Our goal for this question is to maintain a min-heap of k length
        
        public int FindKthLargest(int[] nums, int k)
        {
            SortedSet<(int,int)> heap = new SortedSet<(int,int)>();
            for (int i = 0; i < nums.Length; i++)
            {
                heap.Add((nums[i], i)); // We need to add the index to avoid repetition
                if(heap.Count > k)
                {
                    heap.Remove(heap.Min);
                    // We need to maintain the length of k
                }
            }
            return heap.Min.Item1;
            // If the length of the heap is k, then the peak of heap(Smallest element) is the kth largest element
        }
        #endregion
    }
}
