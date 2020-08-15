using System;
using System.Text.Json.Serialization;

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

    }
}
