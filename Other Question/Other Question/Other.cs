using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Other_Question
{
    class TrieNode
    {
        public bool isWord;
        public TrieNode[] next;
        public TrieNode(int count = 26)
        {
            next = new TrieNode[count];
        }
    }
    #region Leetcode 1032  Stream of Characters
    public class StreamChecker
    {
        readonly TrieNode root;
        readonly StringBuilder sb;
        // Record the recent input
        public StreamChecker(string[] words)
        {
            root = new TrieNode();
            sb = new StringBuilder();

            foreach (string word in words)
            {
                TrieNode node = root;
                int n = word.Length;
                for (int i = n-1; i >= 0; i--)
                {
                    char c = word[i];
                    if(node.next[c-'a'] == null)
                    {
                        node.next[c - 'a'] = new TrieNode();
                    }
                    node = node.next[c - 'a'];
                }
                node.isWord = true;
            }

        }

        public bool Query(char letter)
        {
            sb.Append(letter);
            TrieNode node = root;
            for (int i = sb.Length -1; i >=0 && node != null; i--)
            {
                char c = sb[i];
                node = node.next[c - 'a'];
                if(node != null && node.isWord)
                {
                    return true;
                }
            }
            return false;
        }
    }
    #endregion
    class Other
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 169  Majority Element (Several Solutions)
        public int MajorityElementwithDictionary(int[] nums)
        {
            Dictionary<int, int> count = new Dictionary<int, int>();
            int n = nums.Length;
            foreach (int num in nums)
            {
                if (!count.ContainsKey(num))
                {
                    count[num] = 0;
                }
                if (++count[num] > n / 2) { return num; }
            }
            return -1;
        }
        public int MajorityElementwithRandomnization(int[] nums)
        {
            int n = nums.Length;
            Random r = new Random();
            while (true)
            {
                int index = r.Next(0, n);
                int target = nums[index];
                int count = 0;
                foreach (int num in nums)
                {
                    if (num == target && ++count > n / 2) { return target; }
                }
            }
        }
        public int MajorityElementwithBinaryVoting(int[] nums)
        {
            // Since a single number occured more than n/2 time, the count of 0/1 on bit[i] are also more than n/2
            int n = nums.Length;
            int majority = 0;
            for (int i = 0; i < 32; i++)
            {
                int count = 0;
                int mask = 1 << i;
                foreach (int num in nums)
                {
                    if ((mask & num) != 0) { ++count; }
                }
                if (count > n / 2)
                {
                    majority |= mask;
                }
                // Otherwise, if there is more 0, we just have to keep majority's bit[i] as 0
            }
            return majority;
        }
        public int MajorityElement(int[] nums)
        {
            int majority = nums[0];
            int count = 0;
            foreach (int num in nums)
            {
                if (num == majority) { ++count; }
                else if (--count == 0)
                {
                    count = 1;
                    majority = num;
                }
            }
            return majority;
        }

        #endregion
        #region Leetcode 520  Detect Capitals
        public bool DetectCapitalUse(string word)
        {
            int n = word.Length;
            bool start = Char.IsUpper(word[0]);
            if (n == 1) { return true; }
            else if (Char.IsUpper(word[0]) && Char.IsUpper(word[1])) // It must be all upper case character
            {
                for (int i = 2; i < n; ++i)
                {
                    if (Char.IsLower(word[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                // The rest of the word must be all lower
                for (int i = 1; i < n; ++i)
                {
                    if (Char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }

            }
            return true;
            // It passed all the test cases

        }
        #endregion
        #region Leetcode 125 Valid Palindrome
        public static bool IsPalindrome(string s)
        {
            StringBuilder temp = new StringBuilder("");
            foreach (char c in s)
            {
                if (Char.IsLetter(c))
                {
                    temp.Append(Char.ToLower(c));
                }
            }
            s = temp.ToString();
            int n = s.Length;
            int l; int r;
            if (n % 2 == 1)
            {
                l = n / 2 + 1;
                r = l;
            }
            else
            {
                l = n / 2;
                r = n / 2 + 1;
            }
            while (l >= 0 && r < n && s[l] == s[r])
            {
                --l;
                ++r;
            }
            return r - l == n;
        }
        #endregion
        #region Leeetcode 211   Add and Search Word - Data structure design
        public class WordDictionary
        {
            private class TrieNode
            {
                public TrieNode[] children;
                public bool isWord;
                public string word;

                public TrieNode()
                {
                    children = new TrieNode[26];
                    word = "";
                    isWord = false;
                }
            }
            readonly TrieNode root;
            /** Initialize your data structure here. */
            public WordDictionary()
            {
                root = new TrieNode();
            }

            /** Adds a word into the data structure. */
            public void AddWord(string word)
            {
                TrieNode node = root; // We start adding words based on the root
                for (int i = 0; i < word.Length; i++)
                {
                    int index = word[i] - 'a';
                    if (node.children[index] == null)
                    {
                        node.children[index] = new TrieNode();
                    }
                    // We shift the pointer to the next level
                    node = node.children[index];
                }
                node.isWord = true;
                node.word = word;
            }

            /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
            public bool Search(string word)
            {
                return dfs(root, 0, word);
            }
            private bool dfs(TrieNode cur, int index, string word)
            {

                if (cur == null)
                // There is no way to go
                {
                    return false;
                }
                else if (index == word.Length)
                // Successfully searched through the entire word
                {
                    return cur.isWord;
                }
                if (word[index] == '.')
                {
                    foreach (TrieNode child in cur.children)
                    {
                        if (dfs(child, index + 1, word))
                        {
                            return true;
                        }
                    }
                }
                else
                // It is a character
                {
                    int cur_index = word[index] - 'a';
                    TrieNode next = cur.children[cur_index];
                    return dfs(next, index + 1, word);
                }
                return false;
            }
        }

        #endregion
        #region Leetcode 442  Find All Duplicates in an Array
        public IList<int> FindDuplicates(int[] nums)
        {
            IList<int> ans = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[Math.Abs(nums[i] - 1)] < 0)
                {
                    ans.Add(Math.Abs(nums[i]));
                }
                else
                {
                    nums[Math.Abs(nums[i] - 1)] *= -1;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 397  Integer Replacement
        public int IntegerReplacement(int n)
        {
            if (n == int.MaxValue)
            {
                return 32;
            }
            int ans = 0;
            while (n > 1)
            {
                if (n % 2 == 0)
                {
                    n /= 2;
                }
                else
                {
                    /* When n is odd, it could be written as 2k+1
                     * In other word, n-1 becomes 2k and n+1 becomes 2k+2
                     * Therefore, (n-1)/2 = k and (n+1)/2 = k+1 and one of them is even(ideal situation)
                     * In conclusion, we are going to select based on whether n+1 could be divided by 4, because if so, we can do two division in a row
                     * Special Case: when n == 3, we should make it 2 instead of 4
                     */
                    if ((n + 1) % 4 == 0 && n != 3)
                    {
                        ++n;
                    }
                    else
                    {
                        --n;
                    }
                }
                ++ans;
            }
            return ans;
        }
        #endregion
        #region Leetcode 679  24 Game
        public bool JudgePoint24(int[] n)
        {
            List<double> nums = new List<double>();
            for (int i = 0; i < n.Length; i++)
            {
                nums.Add(n[i] * 1.0);
            }
            return Helper24(nums);
        }
        public bool Helper24(List<double> nums)
        {
            int n = nums.Count;
            if (n == 1)
            {
                return Math.Abs(nums[0] - 24) <= 1e-6;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    List<double> next = new List<double>();
                    for (int k = 0; k < n; k++)
                    {
                        if (k != i && k != j)
                        {
                            next.Add(nums[i]);
                        }
                    }
                    double a = nums[i];
                    double b = nums[j];

                    next.Add(a + b);
                    if (Helper24(next)) { return true; }
                    next.RemoveAt(n);

                    next.Add(a - b);
                    if (Helper24(next)) { return true; }
                    next.RemoveAt(n);

                    next.Add(a * b);
                    if (Helper24(next)) { return true; }
                    next.RemoveAt(n);

                    if (b != 0)
                    {
                        next.Add(a / b);
                        if (Helper24(next)) { return true; }
                        next.RemoveAt(n);
                    }
                }
            }
            return false;
        }
        #endregion
        #region Leetcode 119  Pascal's Triangle II
        public IList<int> GetRow(int rowIndex)
        {
            IList<int> level = new List<int>();
            level.Add(1);
            if (rowIndex == 0)
            {
                return level;
            }
            level.Add(1);
            for (int i = 0; i < rowIndex - 1; ++i)
            {
                level = updateRow(level);
            }
            return level;
        }
        public IList<int> updateRow(IList<int> level)
        {
            IList<int> cur = new List<int>();
            cur.Add(1);
            for (int i = 0; i < level.Count - 1; ++i)
            {
                cur.Add(level[i] + level[i + 1]);
            }
            cur.Add(1);
            return cur;
        }
        #endregion
        #region Leetcode 274  H-Index
        public int HIndex_BruteForce(int[] citations)
        {
            int n = citations.Length;
            Array.Sort(citations);
            for (int i = 0; i < n; ++i)
            {
                if (citations[i] >= (n - i))
                {
                    return n - i;
                }
            }
            return 0;
        }
        #endregion
        #region Leetcode 1286  Iterator for Combination
        public class CombinationIterator
        {
            private readonly List<string> combinations;
            int index = 0;
            public CombinationIterator(string characters, int n)
            {
                combinations = new List<string>();
                Dfs(characters, 0, n, new StringBuilder(""));
            }

            public string Next()
            {
                return combinations[index++];
            }

            public bool HasNext()
            {
                return index != combinations.Count;
            }
            private void Dfs(string word, int index, int length, StringBuilder path)
            {
                if (path.Length == length)
                {
                    combinations.Add(path.ToString());
                    return;
                }
                // Since the word given is sorted, then the substring we make must be in lexicographical order
                for (int i = index; i < word.Length; ++i)
                {
                    path.Append(word[i]);
                    Dfs(word, i + 1, length, path);
                    path.Remove(path.Length - 1, 1);
                }
            }
        }
        #endregion
        #region Leetcode 1103  Distribute Candies to People
        public int[] DistributeCandies(int candies, int people)
        {
            int[] ans = new int[people];
            int index = 0;
            int cur = 1;
            while (candies > cur)
            {
                ans[index] += cur;
                candies -= cur;
                ++cur;

                if (++index >= people)
                {
                    index = 0;
                }
            }
            ans[index] += candies;
            return ans;
        }
        #endregion
        #region Leetcode 967  Numbers With Same Consecutive Differences
        public int[] NumsSameConsecDiff(int N, int K)
        {
            List<int> path = new List<int>();
            for (int i = N == 1 ? 0 : 1; i <= 9; i++)
            {
                path.Add(i);
                NSCDdfs(path, N, K);
                path.RemoveAt(0);
            }
            return NSCDans.ToArray();
        }
        List<int> NSCDans = new List<int>();
        public void NSCDdfs(List<int> path, int N, int K)
        {
            int n = path.Count;
            if (n == N) { NSCDans.Add(DigitListTurnInteger(path)); return; }
            int last = path[n - 1];
            for (int next = 0; next <= 9; next++)
            {
                int diff = Math.Abs(last - next);
                if (diff == K)
                {
                    path.Add(next);
                    NSCDdfs(path, N, K);
                    path.RemoveAt(n);
                }

            }
        }
        public int DigitListTurnInteger(List<int> digits)
        {
            int num = 0;
            int place = 1;
            for (int i = digits.Count - 1; i >= 0; --i)
            {
                num += place * digits[i];
                place *= 10;
            }
            return num;
        }
        #endregion
        #region Leetcode 824  Goat Latin
        public string ToGoatLatin(string S)
        {
            int cur_a = 1;
            StringBuilder ans = new StringBuilder();
            int index = 0;
            int n = S.Length;
            while (index < n)
            {
                int start = index; int length = 0;
                while (index < n && S[index] != ' ')
                {
                    index++;
                    length++;
                }
                ans.Append(GoatLatin_Operate(S.Substring(start, length)));
                for (int i = 0; i < cur_a; ++i)
                {
                    ans.Append("a");
                }
                cur_a++;
                index++;
                ans.Append(" ");
            }
            ans.Remove(ans.Length - 1, 1);
            return ans.ToString();
        }
        public string GoatLatin_Operate(string cur)
        {
            StringBuilder ret = new StringBuilder();
            if (isVowel(cur[0]))
            {
                ret.Append(cur);
            }
            else
            {
                ret.Append(cur.Substring(1));
                ret.Append(cur[0]);
            }
            ret.Append("ma");
            return ret.ToString();
        }
        public bool isVowel(char c)
        {
            c = Char.ToLower(c);
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }
        #endregion
        #region Leetcode 905  Sort Array By Parity
        public int[] SortArrayByParity(int[] A)
        {
            int j = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if(A[i] %2 == 0)
                {
                    int temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                }
                ++j;
            }
            return A;
        }
        #endregion
        #region Leetcode 412 Fizz Buzz
        public IList<string> FizzBuzz(int n)
        {
            var ans = new List<string>();
            for (int i = 1; i <= n; ++i)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    ans.Add("FizzBuzz");
                    continue;
                }
                else if (i % 3 == 0)
                {
                    ans.Add("Fizz");
                    continue;
                }
                else if (i % 5 == 0)
                {
                    ans.Add("Buzz");
                    continue;
                }
                ans.Add(Convert.ToString(i));
            }

            return ans;
        }
        #endregion
        #region Leetcode 470  Implement Rand10() Using Rand7()
        public int Rand7()
        {
            return -1;
        }
        public int Rand10()
        // (Rand7() - 1) * 7 + Rand7() - 1 will generate a random number from 0 to 48
        // However, we can not use (Rand7() - 1) * 8 because this will only make the multiple of 8
        {
            int ret = 40;
            while (ret >= 40)
            {
                ret = (Rand7() - 1) * 7 + Rand7() - 1;
            }
            return ret % 10 + 1;
        }
        #endregion
        #region Leetcode 835  Image Overlap
        public int LargestOverlap(int[][] A, int[][] B)
        {
            int max = 0;

            for (int y = 0; y < A.Length; ++y)
            {
                for (int x = 0; x < A.Length; ++x)
                // Enumerate every possible shift
                {
                    max = Math.Max(max, ShiftAndCount(x, y, A, B));
                    max = Math.Max(max, ShiftAndCount(x, y, B, A));
                }
            }
            return max;
        }
        public int ShiftAndCount(int x, int y, int[][] A, int[][] B)
        // A is shifted based on B, so the index of B always starts from (0,0)
        // parameters x and y means the start of the overlapping area and also the shifting distance
        {
            int count = 0;

            for (int aRow = y,bRow = 0; aRow < A.Length; aRow++,bRow++)  
            {
                for (int aCol = x, bCol = 0; aCol < A.Length; aCol++, bCol++)
                {
                    if (A[aRow][aCol] == 1 && B[bRow][bCol] == 1)
                    {
                        ++count;
                    }
                }
            }
            return count;
        }
        #endregion
        #region Leetcode 290  Word Pattern
        public bool WordPattern(string pattern, string str)
        {
            string[] words = str.Split(" ");
            if (words.Length != pattern.Length)
            {
                return false;
            }
            Dictionary<char, string> match = new Dictionary<char, string>();
            for (int i = 0; i < words.Length; ++i)
            {
                char index = pattern[i];

                if (!match.ContainsKey(index))
                {
                    if (match.ContainsValue(words[i]))
                    {
                        return false;
                    }
                    match[index] = words[i];
                }
                else
                {
                    if (match[index] != words[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
        #region Leetcode 299  Bulls and Cows
        public string GetHint(string secret, string guess)
        {
            int[] count = new int[10];
            // Add if a char appears in the secret string and vice versa 
            int bull = 0, cow = 0;
            for (int i = 0; i < secret.Length; ++i)
            {
                int s = secret[i] - '0';
                int g = guess[i] - '0';
                if (s == g)
                {
                    ++bull;
                }
                else
                {
                    if (count[s]++ < 0) { ++cow; }
                    if (count[g]-- > 0) { ++cow; }
                }
            }
            return bull + "A" + cow + "B";
        }
        #endregion
        #region Leetcode 713  Subarray Product Less Than k
        // Remain a sliding window of product less than k
        // Whenever a new element is introduced, there are more (size of window) combinations
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k == 0)
            {
                return 0;
            }
            int product = 1;
            int count = 0;

            for (int i = 0, j = 0; j < nums.Length; ++j)
            {
                product *= nums[j];
                while (i <= j && product >= k)
                {
                    product /= nums[i++];
                }
                count += j - i + 1;
            }
            return count;
        }
        #endregion
    }
}
