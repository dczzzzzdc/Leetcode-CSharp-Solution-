﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO.IsolatedStorage;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Math_Question
{
    class MathQuestions
    {
        static void Main(string[] args)
        {

        }
        #region Extension: Sum all the digits
        public int SumDigits(int n)
        {
            int sum = 0;
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }
        #endregion
        #region Extension: Sum all binary digits
        public int SumBinaryDigits(int[] digits)
        {
            int sum = 0;
            foreach (int digit in digits)
            {
                sum = (sum << 1) | digit;
            }
            return sum;
        }
        #endregion
        #region Extension: Convert a digit to a target digit in a number
        public int ReplaceDigit(int num,int index, int target)
        {
            string s = Convert.ToString(num);
            int original = (int)Char.GetNumericValue(s[index]);
            int pow = (int)Math.Pow(10,s.Length - index - 1);
            return num + pow * (target - original);
        }
        #endregion
        #region Extension: Find the Greatest Common Divisor
        public static int GCD(int a,int b)
        {
            while(a != 0 && b != 0)
            {
                if(a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a | b;
        }
        public static int GCD(int[] nums)
        {
            return nums.Aggregate(GCD);
        }
        #endregion
        #region Extension: Find the Least Common Multiple
        public static int LCM(int a,int b)
        {
            return a * b / GCD(a, b);
        }
        public static int LCM(int[] nums)
        {
            int result = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                result = LCM(result, nums[i]);
            }
            return result;
        }
        #endregion
        #region Extension: Find all the primes within n
        public bool[] FindPrimes(int n)
        {
            bool[] primes = new bool[n + 1];
            Array.Fill(primes, true);
            for (int p = 2; p * p <= n ; p++)
            {
                if (primes[p])
                {
                    for (int i = p * p; i <= n; i+=p)
                    {
                        primes[i] = false;
                    }
                }
            }
            return primes;
        }
        #endregion

        #region Greatest Common Divisor
        public int GreatestCommonDivisor(int a,int b)
        {
            int i;
            for (i = Math.Min(a,b); i >= 1; i--)
            {
                if(a % i == 0 && b % i == 0)
                {
                    break;
                }
            }
            return i;
        }
        #endregion
        #region Leetcode 7  Reverse Integers
        public int Reverse(int x)
        {
            int ans = 0;
            while (x != 0)
            {
                int temp = ans * 10 + x % 10;
                if (temp / 10 != ans) // If temp overflows MaxValue, it will become MinValue
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
                if (roman[s[i]] < roman[s[i + 1]])
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
            while (i >= 0 || j >= 0)
            {
                int sum = carry;
                if (i >= 0)
                {
                    sum += a[i--] - '0';
                }
                if (j >= 0)
                {
                    sum += b[j--] - '0';
                }
                // In base 10, if we have 14, our current sum is 14 % 10 = 4
                // Our carry is 14 / 10
                res.Append(sum % 2);
                carry = sum / 2;

            }
            if (carry != 0)
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
        #region Leetcode 171  Excel Sheet Column Number
        public int TitleToNumber(string s)
        {
            int res = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                res *= 26;
                res += (s[i] - 'A' + 1);
            }
            return res;
        }
        #endregion
        #region Leetcode 172  Factorial Trailing Zero
        public int TrailingZeroes(int n)
        {
            // A trailing zero can only be made 
            // In a n! operation, there is always more 2 than 5
            // Therefore, we just have to count the amount of 5
            return n == 0 ? 0 : n / 5 + TrailingZeroes(n / 5);
        }
        #endregion
        #region Leetcode 258  Add Digits
        public int AddDigits(int num)
        {
            while (num % 10 != 0)
            {
                num = SumDigits(num);
            }
            return num;
        }
        #endregion
        #region Leetcode 313  Super Ugly Digits
        public int NthSuperUglyNumber(int n, int[] primes)
        {
            int[] ugly = new int[n];
            int[] index = new int[primes.Length];
            ugly[0] = 1;
            for (int i = 1; i < n; ++i)
            {
                ugly[i] = int.MaxValue;
                for (int j = 0; j < primes.Length; ++j)
                {
                    ugly[i] = System.Math.Min(ugly[i], primes[j] * ugly[index[j]]);
                }
                for (int k = 0; k < primes.Length; ++k)
                {
                    if (ugly[i] >= primes[k] * ugly[index[k]])
                    {
                        ++index[k];
                    }
                }
            }
            return ugly[n - 1];
        }
        #endregion
        #region Leetcode 326  Power of Three
        public bool IsPowerOfThree(int n)
        {
            if (n < 1)
            {
                return false;
            }
            while (n % 3 == 0)
            {
                n /= 3;
            }
            return n == 1;
        }
        #endregion
        #region Leetcode 202  Happy Number
        public bool IsHappy(int n)
        {
            // Using Floyd's Cycle Detection(Two Pointers)
            if (n == 1) { return true; }
            int fast = SumDigitSquare(n);
            int slow = n;
            while (slow != fast)
            {
                if (fast == 1 || slow == 1) { return true; }
                slow = SumDigitSquare(slow);
                fast = SumDigitSquare(SumDigitSquare(fast));

            }
            return false;
        }
        public int SumDigitSquare(int n)
        {
            int sum = 0;
            while (n != 0)
            {
                int cur = n % 10;
                sum += cur * cur;
                n /= 10;
            }
            return sum;
        }
        #endregion
        #region Leetcode 367  Valid Perfect Square
        // 1 + 3 + 5 + ... + (2i - 1) = (2i - 1 + 1) * (i/2) = i * i
        public bool IsPerfectSquare(int n)
        {
            int i = 1;
            while (n > 0)
            {
                n -= i;
                i += 2;
            }
            return n == 0;
        }
        #endregion
        #region Leetcode 517  Super Washing Machine
        public int FindMinMoves(int[] machines)
        {
            int sum = machines.Sum();
            int n = machines.Length;
            if(sum % n != 0) { return -1; }
            int target = sum / n;
            int over = 0;
            int ans = 0;
            foreach(int m in machines)
            {
                over += m - target;
                ans = Math.Max(ans, Math.Max(m - target, Math.Abs(over)));
                // If over is positive, than we definitely need to do more than over operations
                // If over is negative, then we do not really need over operations 
                // since clothes can be transfered from two of its adjacent machines

                // If the current machine has more clothes than its target, then it also needs more than m - target operations

            }
            return ans;
        }
        #endregion
        #region Leetcode 400  Nth Digit
        // From 1 ~ 9, there are 9 one digit numbers = 1 * 9 digits
        // From 10 ~ 99, there are 90 two digits numbers = 2 * 90 digit

        // After the while loop, we know that our target digit is the nth(after loop) digit in all (len) digit number

        // Then, we do start += (n-1) / len to specify which number is it

        // Finally, we mod(n-1) with len to find the actual index
        public int FindNthDigit(int n)
        {
            long range = 9;
            int len = 1;
            int start = 1;
            // The start of the range
            while (n > len * range)
            {
                n -= (int)(len * range);
                ++len;
                range *= 10;
                start *= 10;
            }
            // Since we are referring to the index, we should use n-1

            // For example, we are trying to the third digit in the 3-digits numbers
            // The start should be 100 instead of 101
            // And the index of the third digit should be 2, which is obtained by (3-1) % 3

            start += (n - 1) / len;
            return (int)Char.GetNumericValue(Convert.ToString(start)[(n - 1) % len]);
        }
        #endregion
        #region Leetcode 413  Arithmetic Slices
        public int NumberOfArithmeticSlices(int[] A)
        {
            // dp[i] the amount of slices ending with A[i]
            int ans = 0;
            int count = 0;
            // count represents dp[i-1]
            for (int i = 2; i < A.Length; ++i)
            { // We at least need three numbers to form a slice
                if (A[i] - A[i - 1] == A[i - 1] - A[i - 2])
                {
                    ++count;
                    ans += count;
                }
                else
                {
                    count = 0;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 423  Reconstruct Original Digits from English
        public string OriginalDigits(string s)
        {
            int[] count = new int[10];
            foreach(char c in s)
            {
                if(c == 'z')
                {
                    count[0]++;
                }
                if(c == 'w')
                {
                    count[2]++;
                }
                if(c == 'x')
                {
                    count[6]++;
                }
                if(c == 's')
                {
                    count[7]++;
                    // six & seven
                    // Therefore, actual seven count + count[6] = count[7]
                }
                if(c == 'g')
                {
                    count[8]++;
                }
                if(c == 'u')
                {
                    count[4] ++;
                }
                if(c == 'f')
                {
                    count[5]++;
                    // five & four
                }
                if(c == 'h')
                {
                    count[3]++;
                    // three & eight
                }
                if(c == 'o')
                {
                    count[1]++;
                    // one & four & two & zero
                }
                if(c == 'i')
                {
                    count[9]++;
                    // nine & eight & six & five
                }
            }
            // Offset
            count[7] -= count[6];
            count[5] -= count[4];
            count[3] -= count[8];
            count[1] -= (count[4] + count[2] + count[0]);
            count[9] -= (count[8] + count[6] + count[5]);

            StringBuilder res = new StringBuilder();

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    res.Append(i);
                }
            }
            return res.ToString();
        }
        #endregion
        #region Leetcode 453  Minimum Moves to Equal Array Elements
        public int MinMoves(int[] nums)
        {
            // Adding 1 to n-1 elements is the same as reducing 1 from 1 element
            // Therefore, the best way is to make all other element the same as the min elment
            if (nums.Length == 0)
            {
                return 0;
            }
            int min = int.MaxValue;
            foreach (int n in nums)
            {
                min = Math.Min(min, n);
            }
            int ans = 0;
            foreach (int n in nums)
            {
                ans = ans + n - min;
            }
            return ans;
        }
        #endregion
        #region Leetcode 507  Perfect Number
        public bool CheckPerfectNumber(int num)
        // There is several pairs of divisors, i and num / i
        {
            if (num <= 1) { return false; }
            int sum = 1;
            for (int i = 2; i * i < num; ++i)
            {
                if (num % i == 0)
                {
                    sum += i + num / i;
                }
            }
            return sum == num;
        }
        #endregion
        #region Leetcode 523  Continuous Subarray Sum
        // Core theory (sum1 - sum2) = n * k ==> (sum1 - sum2) % k = 0==> sum1 % k = sum2 % k
        public bool CheckSubarraySum(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            // Key: Index
            // Value: Running sum
            map[0] = -1;
            // If the original array is our target, then n-1 - (-1) is able to find the entire array
            int sum = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i];
                if (k != 0)
                {
                    sum %= k;
                }

                if (map.ContainsKey(sum))
                {
                    if (i - map[sum] >= 2)
                    // The subarray is long enough
                    {
                        return true;
                    }
                }
                else
                {
                    map[sum] = i;
                }
            }
            return false;

        }
        #endregion
        #region Leetcode 319  Bulb Switcher
        // Core Observation:
        // After even times of toggling, the state will not change 
        // In this case, would still be on 

        // The sixth bulb will be toggled on round 2 & 3 & 6
        // Therefore, a bulb will be toggled on its factor(excluding one) round

        // In conclusion, the ith bulb will be toggled for k - 1 times, where k is the amount of factors it have
        // In other words, k must be odd to make k - 1 even
        // In fact, only perfect square numbers have an odd k
        // Eg. 25 have 3 factor (1,5,25)

        // Finally, this question is transfered to finding the amount of perfect square number smaller than n
        // Which is exactly Math.Sqrt(n)

        public int BulbSwitch(int n)
        {
            return (int)(Math.Sqrt(n));
        }
        #endregion
        #region Leetcode 537  Complex Number Multiplication
        public string ComplexNumberMultiply(string a, string b)
        {
            StringBuilder res = new StringBuilder();

            string[] A = a.Split("+");
            string[] B = b.Split("+");

            int a1 = Convert.ToInt32(A[0]);
            int a2 = Convert.ToInt32(B[0]);
            int b1 = Convert.ToInt32(A[1].Replace('i', ' '));
            int b2 = Convert.ToInt32(B[1].Replace('i', ' '));

            int a1a2 = a1 * a2;
            int b1b2 = b1 * b2;
            // This must be in a form of n * i * i, which is -n
            res.Append(a1a2 - b1b2).Append('+');

            int a1b2 = a1 * b2;
            int a2b1 = a2 * b1;
            res.Append(a1b2 + a2b1).Append('i');

            return res.ToString();
        }
        #endregion
        #region Leetcode 598  Range Addition II
        // We are trying to find the intersection area of each operation
        public int MaxCount(int m, int n, int[][] ops)
        {
            foreach (int[] op in ops)
            {
                m = Math.Min(m, op[0]);
                n = Math.Min(n, op[1]);
            }
            return m * n;
        }
        #endregion
        #region Leetcode  633 Sum of Square Numbers
        public bool JudgeSquareSum(int c)
        {
            for (long a = 0; a * a <= c; ++a)
            {
                double b = Math.Sqrt(c - a * a);
                if (b == (int)b)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region Leetcode 620  Solve the Equation
        public string SolveEquation(string s)
        {
            string[] equation = s.Split("=");
            var left = evaluateExpression(equation[0]);
            var right = evaluateExpression(equation[1]);
            int x = left.Item2 - right.Item2;
            int value = right.Item1 - left.Item1;
            if (x == 0)
            {
                if (value == 0)
                {
                    return "Infinite solutions";
                }
                return "No solution";
            }
            StringBuilder res = new StringBuilder();
            res.Append('x').Append('=').Append((int)(value / x));
            return res.ToString();
        }
        private (int, int) evaluateExpression(string s)
        {
            int value = 0;
            int x = 0;

            char[] operators = { '+', '-' };
            List<string> tokens = s.Split(operators).ToList();
            for (int i = 0; i < tokens.Count; i++)
            {
                if(tokens[i] == "")
                {
                    tokens.RemoveAt(i);
                }
            }
            operators = new char[tokens.Count];
            int index = 0;
            if (s[0] != '-')
            {
                operators[0] = '+';
                ++index;
            }

            foreach (char c in s)
            {
                if (c == '+' || c == '-')
                {
                    operators[index++] = c;
                }
            }
            index = 0;
            foreach (string token in tokens)
            {
                bool plus = operators[index++] == '+';
                if (token == "x")
                {
                    if (plus) { ++x; }
                    else { --x; }
                }
                else if (token.Contains("x"))
                {
                    int count = Convert.ToInt32(token.Substring(0, token.IndexOf("x")));
                    if (plus) { x += count; }
                    else { x -= count; }
                }
                else if (int.TryParse(token, out _))
                {
                    int add_value = Convert.ToInt32(token);
                    if (plus) { value += add_value; }
                    else { value -= add_value; }
                }
            }

            return (value, x);
        }
        #endregion
        #region Leetcode 1551  Minimum Operations to Make Array Equal
        public int MinOperations(int n)
        {
            int op = 0;
            for (int i = n % 2 == 0 ? n + 1 : n; i <= 2 * n - 1; i += 2)
            {
                op += i - n;
            }
            return op;
        }
        #endregion
        #region Leetcode 1513  Number of Substrings With Only 1s
        public int NumSub(string s)
        {
            int count = 0;
            int prev = 0;
            int mod = (int)Math.Pow(10, 9) + 7;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    prev = 0;
                }
                else
                {
                    prev++;
                    count += prev;
                    if (count > mod) { count %= mod; }
                }
            }

            return count;
        }
        #endregion
        #region Leetcode 1512  Number of Good Pairs
        public int NumIdenticalPairs(int[] nums)
        {
            Dictionary<int, int> count = new Dictionary<int, int>();
            int ans = 0;
            foreach (int n in nums)
            {
                if (!count.ContainsKey(n))
                {
                    count[n] = 0;
                }
                ans += count[n]++;
            }
            return ans;
        }
        #endregion
        #region Leetcode 1523  Count Odd Numbers in an Interval Range
        public int CountOdds(int low, int high)
        {
            // There will be 1 odd in every 2 numbers
            return (low % 2 == 1 || high % 2 == 1) ? 1 + (high - low) / 2 : (high - low) / 2;
        }
        #endregion
        #region Leetcode 1524  Number of Sub-arrays With Odd Sum
        public int NumOfSubarrays(int[] arr)
        {
            int ans = 0;
            int mod = (int)(Math.Pow(10, 9)) + 7;
            (int, int) even_odd = (0, 0);
            foreach (int num in arr)
            {
                int even = even_odd.Item1;
                int odd = even_odd.Item2;
                if (num % 2 == 0)
                // Adding an even number
                {
                    // This will generate a new even subarray which is the number itself
                    even_odd = (even + 1, odd);
                }
                else
                // Adding an odd number
                { 
                    even_odd = (odd, even + 1);
                }
                ans = (ans + even_odd.Item2) % mod;
            }

            return ans;
        }
        #endregion
        #region Leetcode 1497  Check If Array Pairs Are Divisible by k
        // For positive number, the sum of mudulo of two numbers must be k or 0
        // It is a little more complex for negative numbers
        // For example, when k = 3, we can pair 1 or -2 with 2
        // THerefore, 1 and -2 has the equal distance of ((-2 % 3) + 3) % 3 = 1, which is ((num % k) + k) % k to prevent a negative modulo
        public bool CanArrange(int[] arr, int k)
        {
            Dictionary<int, int> modulo = new Dictionary<int, int>();
            for (int i = 0; i <= k; ++i)
            {
                modulo[i] = 0;
            }
            foreach (int x in arr)
            {
                modulo[((x % k) + k) % k]++;
            }
            if (modulo[0] % 2 != 0)
            {
                return false;
            }
            for (int i = 1; i < k; i++)
            {

                if (modulo[i] != modulo[k - i])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
        #region Leetcode 1492  The kth Factor of n
        public int KthFactor(int n, int k)
        {
            int d = 1;
            // Checking the smaller factors, such as 1 2 3 for 12
            for (d = 1; d * d <= n; ++d)
            {
                if (n % d == 0 && --k == 0)
                {
                    return d;
                }
            }
            // Checking the larger factors, such as 4 6 12 for 12
            for (d = d - 1; d >= 1; --d)
            {
                if (d * d == n)
                {
                    // We have already counted d once in the previous loop
                    continue;
                }
                if (n % d == 0 && --k == 0)
                {
                    return n / d;
                }
            }
            return -1;
        }
        #endregion
        #region Leetcode 1442  Count Triplets That Can Form Two Arrays of Equal XOR
        public int CountTriplets(int[] arr)
        {
            int n = arr.Length;
            int ans = 0;
            int[] prefix = new int[n];
            prefix[0] = arr[0];
            for (int i = 1; i < n; ++i)
            {
                prefix[i] = prefix[i - 1] ^ arr[i];
            }

            for (int i = 0; i < n; ++i)
            {
                if (prefix[i] == 0)
                {
                    ans += i;
                }
                for (int j = i - 1; j >= 0; --j)
                {
                    if (prefix[i] == prefix[j])
                    {
                        ans += i - j - 1;
                    }
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 1323  Maximum 69 Number
        public int Maximum69Number(int num)
        {
            checked
            {
                int index = 0;
                int first = -1;
                int n = num;
                while (n != 0)
                {
                    int digit = n % 10;
                    n /= 10;
                    if(digit == '6')
                    {
                        first = index;
                        break;
                    }
                    ++index;
                }
                if (first == -1)
                {
                    return num;
                }

                int pow = (int)Math.Pow(10, first);
                return num + 3 * pow;
            }
            
        }
        #endregion
        #region Leetcode 1447  Simplified Fractions
        public IList<string> SimplifiedFractions(int n)
        {
            IList<string> ans = new List<string>();
            if (n <= 1) { return ans; }

            for (int i = 2; i <= n; ++i)
            {
                for (int j = 1; j < i; ++j)
                {
                    if (GreatestCommonDivisor(i,j) == 1)
                    {
                        StringBuilder fraction = new StringBuilder();
                        fraction.Append(j).Append("/").Append(i);
                        ans.Add(fraction.ToString());
                    }
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 1344  Angle Between Hands of a Clock
        // The hour pointer moves 360/12 = 30 degrees per hour,0.5 degree per minute
        // The minute pointer moves 360/60 = 6 degrees per minute
        public double AngleClock(int h, int m)
        {
            double minute = m * 6.0;
            double hour = m * 0.5;
            if(h != 12) { hour += h * 30.0; }
            double diff = Math.Abs(hour - minute);
            return Math.Min(diff, 360 - diff);
        }
        #endregion
        #region Leetcode 1232  Check If It Is a Straight Line
        // If all the points are on the same line, then the slope value between any two points must be the same
        // Slope = (x1 - x2) / (y1 - y2)
        // Therefore, (x1 - x) / (y1 - y) = (x1 - x2) / (y1 - y2) => 
        // (x1 - x) * dy = (y1 - y) * dx
        public bool CheckStraightLine(int[][] points)
        {
            int n = points.Length;
            if (n == 2)
            {
                return true;
            }
            int x0 = points[0][0]; int y0 = points[0][1];
            int x1 = points[1][0]; int y1 = points[1][1];
            int dx = x1 - x0; int dy = y1 - y0;
            for (int i = 2; i < n; ++i)
            {
                int x = points[i][0];
                int y = points[i][1];
                if (dx * (y-y1) != dy * (x-x1))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region Leetcode 1317  Convert Integer to the Sum of Two No-Zero Integers
        public int[] GetNoZeroIntegers(int n)
        {
            for (int i = 1; i <= n / 2; ++i)
            {
                if (isNoZeroInt(i) && isNoZeroInt(n - i))
                {
                    return new int[2] { i, n - i };
                }
            }
            return null;
        }
        public bool isNoZeroInt(int n)
        {
            while (n != 0)
            {
                int digit = n % 10;
                if (digit == 0)
                {
                    return false;
                }
                n /= 10;
            }
            return true;
        }
        #endregion
        #region Leetcode 1281  Subtract the Product and Sum of Digits of an Integer
        public int SubtractProductAndSum(int n)
        {
            int sum = 0;
            int product = 1;
            while (n != 0)
            {
                int digit = n % 10;
                sum += digit;
                product *= digit;
                n /= 10;
            }
            return product - sum;
        }
        #endregion
        #region Leetcode 1276  Number of Burgers with No Waste of Ingredients
        // If small = X and jumbo = Y
        // x + Y = cheese & 2X + 4Y = tomato
        // X + Y = cheese & X + 2Y = tomato / 2

        // Therefore, Y = tomato/2 - cheese & X = cheese - Y = cheese - tomato / 2 + cheese = 2 * cheese - tomato / 2

        public IList<int> NumOfBurgers(int t, int c)
        {
            if(t % 2 == 0 && c * 2 <= t && t <= c * 4)
            // Making sure that the result is not a negative number
            {
                return new List<int> { t / 2 - c, 2 * c - t / 2 };
            }
            return new List<int>();
        }

        #endregion
        #region Leetcode 1253  Reconstruct a 2-Row Binary Matrix
        public IList<IList<int>> ReconstructMatrix(int upper, int lower, int[] c)
        {
            int[][] grid = new int[2][];
            int n = c.Length;
            grid[0] = new int[n];
            grid[1] = new int[n];
            for (int i = 0; i < n; ++i)
            {
                if (c[i] == 2)
                {
                    grid[0][i] = 1;
                    grid[1][i] = 1;
                    --upper;
                    --lower;
                    if (upper < 0 || lower < 0)
                    {
                        return new List<IList<int>>();
                    }
                }
            }
            for (int i = 0; i < n; ++i)
            {
                if (c[i] == 1)
                {
                    if (upper > 0)
                    {
                        --upper;
                        grid[0][i] = 1;
                    }
                    else if (lower > 0)
                    {
                        --lower;
                        grid[1][i] = 1;
                    }
                    else
                    {
                        return new List<IList<int>>();
                    }
                }
            }
            if (upper != 0 || lower != 0)
            {
                return new List<IList<int>>();
            }
            var ret = new List<IList<int>>();
            foreach (int[] row in grid)
            {
                ret.Add(row.ToList());
            }
            return ret;
        }
        #endregion
        #region Leetcode 949  Largest Time for Given Digits
        public string LargestTimeFromDigits(int[] A)
        {
            int max = -1;

            for (int h1 = 0; h1 < 4; h1++)
            {
                for (int h2 = 0; h2 < 4; h2++)
                {
                    if (h1 != h2)
                    {
                        for (int m1 = 0; m1 < 4; m1++)
                        {
                            if (m1 != h1 && m1 != h2)
                            {
                                int m2 = 6 - m1 - h1 - h2;

                                max = Math.Max(max, FormLegalTime(A[h1], A[h2], A[m1], A[m2]));
                            }
                        }
                    }
                }
            }
            if (max == -1)
            {
                return "";
            }
            return string.Format("{0:D2}:{1:D2}", max / 60, max % 60);
            // D2 makes one digit numbers two digit decimals
            // For example, 6 becomes 06
        }
        public int FormLegalTime(int a, int b, int c, int d)
        {
            int hour = a * 10 + b;
            int minute = c * 10 + d;
            if (hour < 24 && minute < 60)
            {
                return hour * 60 + minute;
            }
            return -1;
        }
        #endregion
        #region Leetcode 1217  Minimum Cost to Move Chips to The Same Position
        // Find goal is to make all the position have the same parity
        public int MinCostToMoveChips(int[] position)
        {
            int even = 0;
            int odd = 0;
            foreach (int chip in position)
            {
                if (chip % 2 == 0)
                {
                    even++;
                }
                else
                {
                    odd++;
                }
            }
            return Math.Min(even, odd);
        }
        #endregion
        #region Leetcode 165  Compare Version Numbers
        public int CompareVersion(string v1, string v2)
        {
            if (v1 == v2)
            {
                return 0;
            }

            string[] one = v1.Split(".");
            string[] two = v2.Split(".");

            int l1 = one.Length, l2 = two.Length;
            for (int i = 0; i < Math.Max(l1, l2); ++i)
            {
                int cur1 = i < l1 ? ConvertToVersionNumber(one[i]) : 0;
                int cur2 = i < l2 ? ConvertToVersionNumber(two[i]) : 0;

                int compare = cur1.CompareTo(cur2);
                if (compare != 0)
                {
                    return compare;
                }
            }
            return 0;
        }
        public int ConvertToVersionNumber(string s)
        {
            int index = 0;
            while (index < s.Length && s[index] == '0')
            {
                ++index;
            }
            int num = 0;
            while (index < s.Length && Char.IsNumber(s[index]))
            {
                num *= 10;
                num += (int)(s[index++]);
            }
            return num;
        }
        #endregion
        #region Leetcode 1390  Four Divisors
        public int SumFourDivisors(int[] nums)
        {
            int sum = 0;
            foreach (int n in nums)
            {
                CountFactors(n, ref sum);
            }
            return sum;
        }
        public void CountFactors(int n, ref int sum)
        {
            int count = 0;
            int cur = 0;
            for (int i = 1; i * i <= n; ++i)
            {
                if (n % i != 0)
                {
                    continue;
                }
                count++;
                cur += i;
                if (i != n / i)
                {
                    count++;
                    cur += n / i;
                }
            }
            if (count == 4)
            {
                sum += cur;
            }
        }
        #endregion
        #region Leetcode 1362  Closest Divisors
        public int[] ClosestDivisors(int num)
        {
            int max1 = 0;
            for (int i = 1; i * i <= num + 1; ++i)
            {
                if ((num + 1) % i == 0)
                {
                    max1 = i;
                }
            }
            int max2 = 0;
            for (int i = 1; i * i <= num + 2; ++i)
            {
                if ((num + 2) % i == 0)
                {
                    max2 = i;
                }
            }
            if ((num + 1) / max1 - max1 < (num + 2) / max2 - max2)
            {
                return new int[2] { max1, (num + 1) / max1 };
            }
            else
            {
                return new int[2] { max2, (num + 2) / max2 };
            }
        }
        #endregion
        #region Leetcode 1154  Day of the Year
        public int DayOfYear(string s)
        {
            int year = Convert.ToInt32(s.Substring(0, 4));
            int month = Convert.ToInt32(s.Substring(5, 2));
            int day = Convert.ToInt32(s.Substring(8));
            return Days(month, year) + day;
        }
        public int Days(int month, int year)
        {
            int day = 0;
            for (int i = month - 1; i > 0; --i)
            // month - 1 because if it is February, the timed only goes through January
            {
                if (i == 2)
                {
                    day += 28;
                    if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                    {
                        ++day;
                    }
                }
                else if (i == 4 || i == 6 || i == 9 || i == 11)
                {
                    day += 30;
                }
                else
                {
                    day += 31;
                }
            }
            return day;
        }
        #endregion
        #region Smallest Range Series
        // Smallest Range I
        public int SmallestRangeI(int[] A, int K)
        {
            int min = A[0];
            int max = A[0];
            foreach (int n in A)
            {
                min = Math.Min(min, n);
                max = Math.Max(max, n);
            }
            return Math.Max(0, max - min - 2 * K);
        }
        #endregion
        #region Leetcode 1041  Robot Bounded in Circle
        public bool IsRobotBounded(string instructions)
        {
            int x = 0, y = 0;
            // Indicates the position of the robot
            int direction = 0;
            foreach(char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'R':
                        direction = (direction + 3) % 4;
                        break;
                    case 'L':
                        direction = (direction + 1) % 4;
                        break;

                    case 'G':
                        switch (direction)
                        {
                            case 0: y++; break;
                            case 1: x++; break;
                            case 2: y--; break;
                            case 3: x--; break;
                        }
                        break;
                }
            }
            // A cycle exist if:
            // It is in its initial position
            // It is facing at a different direction 
            return direction != 0 || (x == 0 && y == 0);
        }
        #endregion
        #region Leetcode 179  Largest Number
        public string LargestNumber(int[] nums)
        {
            string[] snum = nums.Select(x => x.ToString()).ToArray();
            Array.Sort(snum, (string s1,string s2) =>
            {
                string str1 = s1 + s2;
                string str2 = s2 + s1;
                return str2.CompareTo(str1);
            });

            if (snum[0].Equals("0"))
            {
                return snum[0];
            }
            return string.Join("",snum);
        }
        #endregion
        #region Leetcode 781  Rabbits in Forest
        // When a rabbit answers i, then there are i + 1 rabbits that have the same color with it
        public int NumRabbits(int[] answers)
        {
            if(answers.Length == 0)
            {
                return 0;
            }

            int sum = 0;
            Dictionary<int, int> count = new Dictionary<int, int>();
            // DIctionary is used to record the frequency of a specific answer
            // Key: The answer
            // Value: Times it occurs

            foreach(int a in answers)
            {
                if(a == 0)
                {
                    sum++;
                    continue;
                }
                if(!count.ContainsKey(a))
                {
                    count[a] = 1;
                    sum += a + 1;
                }
                else
                {
                    count[a]++;
                    if(count[a] == a + 1)
                    // If all a + 1 rabbits are present, remove them for future calculations
                    {
                        count.Remove(a);
                    }
                }
            }
            return sum;
        }
        #endregion
        #region Leetcode 645  Region Mismatch
        public int[] FindErrorNums(int[] nums)
        {
            int n = nums.Length;
            int[] count = new int[n + 1];
            foreach (int i in nums)
            {
                count[i]++;
            }
            bool f1 = false;
            bool f2 = false;
            int[] ans = new int[2];
            for (int i = 1; i <= n && (!f1 || !f2); ++i)
            {
                if (count[i] == 2)
                {
                    ans[0] = i;
                }
                else if (count[i] == 0)
                {
                    ans[1] = i;
                }
            }
            return ans;
        }
        #endregion
        #region Leetcode 495  Teemo Attacking
        public int FindPoisonedDuration(int[] time, int duration)
        {
            int n = time.Length;
            int sum = n * duration;

            for (int i = 1; i < n; ++i)
            {
                sum -= Math.Max(0, duration - (time[i] - time[i - 1]));
            }
            return sum;
        }
        #endregion
        #region Leetcode 670  Maximum Swap
        //Greedy approach
        public int MaximumSwap(int num)
        {
            char[] n = Convert.ToString(num).ToCharArray();
            int[] last = new int[10];

            for (int i = 0; i < n.Length; ++i)
            {
                last[n[i] - '0'] = i;
            }

            for (int i = 0; i < n.Length; ++i)
            {
                for (int d = 9; d > n[i] - '0'; --d)
                {
                    if (last[d] > i) // Find the last character that was bigger than n[i]
                    {
                        char temp = n[i];
                        n[i] = n[last[d]];
                        n[last[d]] = temp;
                        return Convert.ToInt32(new string(n));
                    }
                }
            }
            return num;
        }
        #endregion
        #region Leetcode 991 Broken Calculator
        // Work this backwards, whenever Y is divisible by 2, we do it
        // Loop until X >= Y, then just do X - Y addition to reach X
        public int BrokenCalc(int X, int Y)
        {
            int count = 0;
            while (Y > X)
            {
                if (Y % 2 == 0)
                {
                    Y /= 2;
                }
                else
                {
                    ++Y;
                }
                ++count;
            }
            return count + X - Y;
        }
        #endregion
        #region Leetcode 592  Fraction Addition and Subtraction
        public string FractionAddition(string expression)
        {
            string[] frs = expression.Replace("-", "+-").Split('+');
            int A = 0, B = 1, a, b;
            foreach (string s in frs)
            {
                if (string.IsNullOrEmpty(s)) continue;

                string[] zm = s.Split('/');

                a = Convert.ToInt32(zm[0]);
                b = Convert.ToInt32(zm[1]);

                A = A * b + a * B;
                B *= b;

                int cd = GCD(Math.Abs(A), B);
                A /= cd;
                B /= cd;
            }
            return string.Format("{0}/{1}", A, B);
        }
        #endregion
    }
    public class Sort2DArray : IComparer<int[]>
    {
        public int Compare(int[] a, int[] b)
        {
            int index = 0;
            int na = a.Length, nb = b.Length;
            int l = Math.Min(na,nb);
            while(index < l)
            {
                if(a[index] > b[index])
                {
                    return 1;
                }
                else if(a[index] < b[index])
                {
                    return -1;
                }
            }

            return na == nb ? 0 : na > nb ? 1 : -1;
        }
    }
}