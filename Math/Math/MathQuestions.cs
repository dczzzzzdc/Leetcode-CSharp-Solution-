using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
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
    }
}