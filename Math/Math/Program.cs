using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Math
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        #region Leetcode 7  Reverse Integers
        public int Reverse(int x)
        {
            int ans = 0;
            while(x != 0)
            {
                int temp = ans * 10 + x % 10;
                if(temp /10 != ans) // If temp overflows MaxValue, it will become MinValue
                {
                    return 0;
                }
                ans = temp;
                x /= 10;
            }
            return ans;
        }
        #endregion
        #region Leetcode 12  Integer to Roman
        public string IntToRoman(int num)
        {
            StringBuilder res = new StringBuilder("");
            string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] value = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int index = 0;
            while (num > 0)
            {
                int count = num / value[index];
                for (int i = 0; i < count; i++)
                {
                    res.Append(roman[index]);
                }
                num %= value[index];
                index++;
            }
            return res.ToString();
        }
        #endregion
        #region Leetcode 13  Roman to Integer
        public int RomanToInt(string s)
        {
            Dictionary<char, int> roman = new Dictionary<char, int>();
            roman.Add('M', 1000);
            roman.Add('D', 500);
            roman.Add('C', 100);
            roman.Add('L', 50);
            roman.Add('X', 10);
            roman.Add('V', 5);
            roman.Add('I', 1);
            int sum = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                // Normally, the current character should be bigger than the right character
                if(roman[s[i]] < roman[s[i + 1]])
                {
                    sum -= roman[s[i]];
                }
                else
                {
                    sum += roman[s[i]];
                }
            }
            // We are not able to calculate the value of the last character in the loop because s[i+1] would be out of bound
            // However, it is guaranteed to be positive
            return sum + roman[s[s.Length - 1]];

        }
        #endregion
        #region Leetcode 67  Add Binary
        public string AddBinary(string a, string b)
        {
            StringBuilder res = new StringBuilder("");
            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;
            while(i>=0 || j >= 0)
            {
                int sum = carry;
                if (i >= 0)
                {
                    sum += a[i--] - '0';
                }
                if(j>= 0)
                {
                    sum += b[j--] - '0';
                }
                // In base 10, if we have 14, our current sum is 14 % 10 = 4
                // Our carry is 14 / 10
                res.Append(sum % 2);
                carry = sum / 2;

            }
            if(carry != 0)
            {
                res.Append(1);
            }
            // In our program, we do the addition from the largest digit while in real life we do it from the smallest one
            // Therefore, we have to reverse our result
            return new string(res.ToString().Reverse().ToArray());
        }
        #endregion
        #region Leetcode 168  Excel Sheet Column Title
        public string ConvertToTitle(int n)
        {
            StringBuilder res = new StringBuilder();
            while (n > 0)
            {
                n--;
                // Note that we are using 'A' + n % 26 but 'A' itself stands for one so we have to n--
                res.Append((char)('A' + n % 26));
                n /= 26;
            }
            
            return new String(res.ToString().Reverse().ToArray());
        }
        #endregion
    }
}
