using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace Heap
{
    public class Program
    {
        private static void Main(string[] args)
        {
        }
    }
    #region MaxHeap Implementation
    class MaxHeap
    {
        int[] data;
        int count;
        public MaxHeap(int n)
        {
            data = new int[n];
        }
        public int GetLeftChild(int index)
        {
            return index * 2 + 1;
        }
        public int GetRightChild(int index)
        {
            return index * 2 + 2;
        }
        public int GetRightValue(int index)
        {
            return data[GetRightChild(index)];
        }
        public int GetLeftValue(int index)
        {
            return data[GetLeftChild(index)];
        }
        private int GetParent(int index)
        {
            return (index -2)/2;
        }
        private int GetParentValue(int index)
        {
            return data[GetParent(index)];
        }
        private bool HasLeftChildren(int index)
        {
            return GetLeftChild(index) < count;
        }
        private bool HasRightChildren(int index)
        {
            return GetRightChild(index) < count;
        }
        public void Enqueue(int item)
        {
            data[count] = item;
            ++count;
            BubbleUp();
        }
        public int Dequeue()
        {
            int result = data[0];
            --count;
            data[0] = data[count];
            data[count] = 0;
            BubbleDown();
            return result;
        }
        private void BubbleUp()
        {
            int cur = count - 1;
            while(GetParent(cur) >= 0 && GetParentValue(cur) < data[cur])
            {
                int parent_index = GetParent(cur);
                Swap(parent_index, cur);
                cur = parent_index;
            }
        }
        private void BubbleDown()
        {
            int index = 0;
            while (HasLeftChildren(index))
            {
                // We choose between two paths to descend
                int max = GetLeftChild(index);
                if(HasRightChildren(index) && GetRightValue(index)>data[max])
                {
                    max = GetRightChild(index);
                }
                if(data[index] >= data[max]) // We no longer need to descend
                {
                    break;
                }
                Swap(index, max);
                index = max;
            }

        }
        public void Swap(int i1,int i2)
        {
            int temp = data[i2];
            data[i2] = data[i1];
            data[i1] = temp;
        }
        public void Print()
        {
            foreach (int item in data)
            {
                Console.Write("{0} {1}",item, " ");
            }
        }
    }
    #endregion
    #region Leetcode 295  public class MedianFinder {

    public class MedianFinder
    {
		
    }
    #endregion
}
