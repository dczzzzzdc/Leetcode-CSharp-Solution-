using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net.Http.Headers;

namespace Heap
{
    public class Program
    {
        private static void Main(string[] args)
        {
            MaxHeap s = new MaxHeap();
            s.Enqueue(1);
            Console.WriteLine(s.count);
        }
    }

    #region MaxHeap Implementation
    class MaxHeap
    {
        private List<int> heap;
        public int count;
        public MaxHeap()
        {
            this.heap = new List<int>();
            this.count = 0;
        }

        private int Left(int i) { return 2 * i + 1; }
        private int GetLeft(int i) { return heap[Left(i)]; }
        private bool HasLeft(int i) { return Left(i) < count; }
        private int Right(int i) { return 2 * i + 2; }
        private int GetRight(int i) { return heap[Right(i)]; }
        private bool HasRight(int i) { return Right(i) < count; }
        private int Parent(int i) { return (i - 1) / 2; }
        private int GetParent(int i) { return heap[Parent(i)]; }

        private void BubbleUp(int index)
        {
            while(Parent(index)>=0 && GetParent(index) < heap[index])
            {
                int parent_index = Parent(index);
                Swap(parent_index, index);
                index = parent_index;
            }
        }

        private void BubbleDown(int index)
        {
            while (HasLeft(index))
            {
                int cur = Math.Max((HasRight(index)?GetRight(index):int.MinValue), GetLeft(index));
                if(heap[index] >= heap[cur]) { break; }
                Swap(index, cur);
                index = cur;
            }
        }
        private void Swap(int i1,int i2)
        {
            int temp = heap[i1];
            heap[i1] = heap[i2];
            heap[i2] = temp;
        }

        public void Enqueue(int x)
        {
            count++;
            heap.Add(x);
            BubbleUp(count - 1);
        }
        public void Dequeue()
        {
            --count;
            heap[0] = heap[count];
            heap[count] = 0;
            BubbleDown(0);
        }
        public int GetMax()
        {
            return heap[0];
        }
        public void Print()
        {
            foreach (int item in heap)
            {
                Console.WriteLine("{0} {1}",item, " ");
            }
        }
    }
    #endregion
    #region Leetcode 295  public class MedianFinder {
    public class MedianFinder
    {
        private SortedSet<int> min = new SortedSet<int>();
        private MaxHeap max = new MaxHeap();
        /** initialize your data structure here. */
        public MedianFinder()
        {
            
        }

        public void AddNum(int num)
        {
            if(max.count == 0)
            {
                max.Enqueue(num);
            }
            else if (max.count == min.Count)
            {
                if (num < max.GetMax())
                {
                    max.Enqueue(num);
                }
                else
                {
                    min.Add(num);
                }
            }
            else if(max.count > min.Count)
            {
                if(num > max.GetMax())
                {
                    min.Add(num);
                }
                else
                {
                    int prev = max.GetMax();
                    min.Add(prev);
                    max.Dequeue();
                    max.Enqueue(num);
                }
            }
            else
            {
                if (num < min.Min)
                {
                    max.Enqueue(num);
                }
                else
                {
                    int prev = min.Min();
                    max.Enqueue(prev);
                    min.Remove(prev);
                    min.Add(num);
                }
            }
        }

        public double FindMedian()
        {
            if(min.Count == max.count)
            {
                return (min.Min + max.GetMax()) / 2;
            }
            else
            {
                return min.Count > max.count ? min.Min : max.GetMax();
            }
        }
    }
    #endregion
}
