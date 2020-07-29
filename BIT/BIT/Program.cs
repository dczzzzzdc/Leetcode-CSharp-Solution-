using System;

namespace BIT
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Bitwise operator
             *  And & (Both)
             *  Only save the bit that are the same in two numbers
             *  
             *  Or | (Either)
             *  If any of the bit is 1, then the result is one
             *  
             *  Xor ^ (Different)
             *  Not ~ (Invert)
             */
            int a = 25;
            int b = 7;
            Console.WriteLine("a: " + Convert.ToString(a, 2).PadLeft(8,'0'));
            Console.WriteLine("b: " + Convert.ToString(b, 2).PadLeft(8,'0'));
            Console.WriteLine("--------");
            Console.WriteLine("And Operation");
            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8,'0'));
        }
    }
}
