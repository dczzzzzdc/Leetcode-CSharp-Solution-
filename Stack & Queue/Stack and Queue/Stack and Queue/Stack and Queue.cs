using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Stack_and_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        #region Leetcode 235/232  Using Stack/Queue to Build a Queue/Stack
        public class MyQueue
        {
            Stack<int> data = new Stack<int>();
            public void Push(int x)
            {
                Stack<int> temp = new Stack<int>();

                while (data.Count != 0)
                {
                    temp.Push(data.Pop());
                }
                temp.Push(x);
                // It is the first data that is going to be accessed with Pop()
                while (temp.Count != 0)
                {
                    data.Push(temp.Pop());
                }
            }
            public int Pop()
            {
                return data.Pop();
            }
            public int Peek()
            {
                return data.Peek();
            }
            public bool Empty()
            {
                return data.Count == 0;
            }
        }
        public class MyStack
        {
            Queue<int> data = new Queue<int>();
            public void Push(int x)
            {
                Queue<int> temp = new Queue<int>();
                temp.Enqueue(x);
                // It is the first element that is going to be accessed with Dequeue()
                while (data.Count != 0)
                {
                    temp.Enqueue(data.Dequeue());
                }
                while (temp.Count != 0)
                {
                    data.Enqueue(temp.Dequeue());
                }
            }
            public int Pop()
            {
                return data.Dequeue();
            }

            /** Get the top element. */
            public int Top()
            {
                return data.Peek();
            }
            public bool Empty()
            {
                return data.Count == 0;
            }
        }
        #endregion
        #region Leetcode 155  Min Stack
        public class MinStack
        {
            Stack<int> data = new Stack<int>();
            Stack<int> min = new Stack<int>();
            // We use another stack to record the min value at every moment
            public void Push(int x)
            {
                data.Push(x);
                if (min.Count ==0) { min.Push(x); }
                else
                {
                    if (x > min.Peek()) { x = min.Peek(); }// We set x to the value of smallest element in the stack
                    // which means that the current min is still the original min
                    min.Push(x);
                }
            }

            public void Pop()
            {
                min.Pop();
                data.Pop();
            }
            public int Top()
            {
                return data.Peek();
            }
            public int GetMin()
            {
                return min.Peek();
            }
        }
        #endregion
        #region Leetcode 946  Valid Stack Sequence
        public bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            Stack<int> s = new Stack<int>();
            // This is a simulation stack
            int n = pushed.Length;
            int pop = 0;
            for (int i = 0; i < n; i++)
            {
                s.Push(pushed[i]);
                while(s.Count!= 0 && s.Peek() == popped[pop])
                {
                    ++pop;
                    s.Pop();
                }
            }
            return s.Count == 0;
            // If the count is 0, then it means that we are able to finish all the tasks
        }
        #endregion
        #region  Leetcode Basic Calculator Series
        
        public int Calculate(string s)
        {
            Queue<char> q = new Queue<char>();
            foreach (char item in s)
            {
                if(item != ' ')
                {
                    q.Enqueue(item);
                }
            }
            q.Enqueue(' '); // This serves as an placeholder
            return CHelper(q);
        }
        public int CHelper(Queue<char> q)
        {
            int num = 0; // The current number we are accumulating 
            int sum = 0;
            int prev = 0; // The previous value that is associated with prevop
            char prevop = '+'; // It is first set as '+' because 0+ does not affect anything 
            while(q.Count != 0)
            {
                char cur = q.Dequeue();
                if(cur - '0'>= 0 && cur - '0' <=9) // It is a number
                {
                    num = num * 10 + cur - '0'; // We should add it to the previous number
                }
                else if (cur == '(')
                {
                    num = CHelper(q);
                }
                else
                // It is an operator so we should start calculating with the previous operator the value
                // Therefore, we need a empty placeholder to make sure that we can calculate with the last operator
                {
                    switch (prevop)
                    {
                        case '+':
                            sum += prev;
                            prev = num;
                            break;
                        case '-':
                            sum += prev;
                            prev = -num; // this means that we are going to minus this number next time 
                            break;
                        // For multiplication and division
                        // We can just easily update the prev value and wait for the next calculation
                        case '*':
                            prev *= num;
                            break;
                        case '/':
                            prev /= num;
                            break;

                    }
                    if(cur == ')')
                    {
                        break;
                    }
                    num = 0; // After a calculation, we reset num to zero
                    prevop = cur; // We update the prevop to the current operator, so then we will use it next time
                }
            }
            return sum + prev;

        }
        #endregion
    }
}
