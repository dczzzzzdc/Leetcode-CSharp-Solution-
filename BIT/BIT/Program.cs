using System;

namespace BIT
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Bitwise operator
             *  And & (Both)
             *  If both two bits are 1, the result is 1
             *  
             *  Or | (Either)
             *  If any of the bit is 1, then the result is 1
             *  
             *  Xor ^ (Different)
             *  If only one of the bits is 1, the result is 1
             * 
             *  Not ~ (Invert)
             *  Revert all the bits
             */
            byte a = 25;
            byte b = 7;
            Console.WriteLine("a: " + Convert.ToString(a, 2).PadLeft(8,'0'));
            Console.WriteLine("b: " + Convert.ToString(b, 2).PadLeft(8,'0'));
            Console.WriteLine("--------");
            Console.WriteLine("And Operation");
            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("Or Operation");
            Console.WriteLine(Convert.ToString(a|b,2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("Xor Operation");
            Console.WriteLine(Convert.ToString(a ^ b,2).PadLeft(8,'0'));
            Console.WriteLine();
            Console.WriteLine("A Not Operation");
            Console.WriteLine(Convert.ToString((byte)~a,2));
        }
    }
}
