using System;
using System.Collections.Generic;

namespace Other_Question
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
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
                    if(num == target && ++ count > n / 2) { return target; }
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
                    if((mask & num) != 0) { ++count; }
                }
                if(count > n / 2)
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
                else if(--count == 0)
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
    }
}
