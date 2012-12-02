using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 100; i++)
            {
                if (i % 3 != 0 || i % 5 != 0)
                {
                    if (i % 3 == 0)
                        Console.WriteLine("Fizz");

                    if (i % 5 == 0)
                        Console.WriteLine("Buzz");

                    continue;
                }

                Console.WriteLine("FizzBuzz");
            }
        }
    }
}
