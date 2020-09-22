using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading;

namespace Greedy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 409  Longest Palindrome
        public int LongestPalindrome(string s)
        {
            int[] count = new int[128];
            int length = 0;
            foreach (char c in s)
            {
                if(++count[c] == 2)
                {
                    count[c] = 0;
                    length += 2;
                }
            }
            return length == s.Length ? length : length + 1;
        }
        #endregion
        #region Leetcode 435 Non-overlapping Intervals
        public int EraseOverlapIntervals(int[][] intervals)
        {
            int n = intervals.Length;
            if (n == 0) { return 0; }
            Array.Sort(intervals, (p1, p2) => {
                return p1[1].CompareTo(p2[1]);
            });
            int ans = 0;
            int r = intervals[0][1];
            // The end of the current session
            for (int i = 1; i < n; i++)
            {
                if (intervals[i][0] >= r)
                // It is outside of the session, so we should update the right bound
                {
                    r = intervals[i][1];
                }
                else
                // It is inside the session, so we does not need it 
                {
                    ++ans;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode
        public bool CarPooling(int[][] trips, int capacity)
        {
            int[] passenger = new int[1001];

            int dest = 0;
            foreach(int[] trip in trips)
            {
                int count = trip[0];
                int start = trip[1];
                int end = trip[2];

                if(start > dest)
                {
                    dest = end;
                }
                passenger[start] += count;
                passenger[end] -= count;
            }

            int cur = 0;
            for(int i = 0; i <= dest; ++i)
            {
                cur += passenger[i];
                if(cur > capacity)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
