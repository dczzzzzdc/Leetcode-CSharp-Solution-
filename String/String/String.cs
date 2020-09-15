using System;
using System.Collections.Generic;

namespace String
{
    class String
    {
        static void Main(string[] args)
        {
            List<int> kmp = KMP("A","ABBA");
            foreach (var item in kmp)
            {
                Console.Write(item + " ");
            }

        }
        #region KMP Algorithm
        // KMP Match

        // Case 1: s[i] == p[j]
        // ++i,++j: Move to check the next pair

        // Case 2: s[i] != p[j], and q is the partial matching string
            // Case 2.1  q is empty: ++i
            // Case 2,2  q = p => We have a full match: ++i & j = next[j] => Treat it as a mismatch
            // Case 2.3  Certain prefix of q = Certain suffix of p: next[j] = LPS(q).Length
            // Case 2.4  Completely no match between the prefix and suffix of q: next[j] must be 0 and skip the next partial match
        public static List<int> KMP(string p, string s)
        {
            int n = s.Length;
            int m = p.Length;
            int[] next = Build(p);
            List<int> ans = new List<int>();
            for (int i = 0,j = 0; i < n; i++)
            {
                while(j > 0 && s[i] != p[j])
                {
                    j = next[j];
                }
                if(s[i] == p[j])
                {
                    ++j;
                }
                if(j == m)
                {
                    ans.Add(i - m + 1);
                    j = next[j];
                }
            }
            return ans;
        }
        private static int[] Build(string p)
        {
            int n = p.Length;
            int[] next = new int[n+1];
            // next[i] = Length of the prefix that is also a suffix in p[0:i-1]

            next[0] = 0; next[1] = 0;
            int index = 2;

            for (int i = 1, j = 0; i < n; i++) // i and j are the pointer to match
            {
                while(j > 0 && p[i] != p[j])
                {
                    j = next[j];
                }
                if(p[i] == p[j])
                {
                    ++j;
                }
                next[index++] = j;
            }
            return next;
        }
        #endregion
        #region Leetcode 459  Repeated Substring Pattern
        // The first char is the start of repeated string
        // The last char is the end of the repeated string

        // S1 = S + S
        // Remove the first and the last char of S1 and make S2
        // Return true if S is in S2 

        // For example, S = abcabc, S1 = abcabcabcabc, S2 = bcabcabcab

        public bool RepeatedSubstringPattern(string s)
        {
            string s1 = s + s;
            string s2 = s1.Substring(1, s1.Length - 2);
            return s2.Contains(s);
        }
        #endregion
        #region Leetcode 763  Partition Labels
        public IList<int> PartitionLabels(string S)
        {
            IList<int> ans = new List<int>();
            Dictionary<char, int> last_occurrence = new Dictionary<char, int>();
            int n = S.Length;
            for (int i = 0; i < n; ++i)
            {
                last_occurrence[S[i]] = i;
            }
            int start = 0;
            int end = 0;
            for (int i = 0; i < n; ++i)
            {
                end = Math.Max(end, last_occurrence[S[i]]);
                // We must extend the current string to contain the last occurrence of the current char
                if (end == i)
                // If the string is long enough and it should end now
                {
                    ans.Add(end - start + 1);
                    start = end + 1;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 58 Length of Last Word
        public int LengthOfLastWord(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return 0;
            }
            int count = 0;
            int i = s.Length - 1;
            while (s[i] == ' ')
            {
                --i;
            }
            for (; i >= 0 && s[i] != ' '; --i)
            {
                ++count;
            }
            return count;
        }
        #endregion
    }
}
