using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork
{
    class First
    {
        public static byte Quantity { get; set; }
        private static IEnumerable<decimal> Fibonacci
        {
            get
            {
                decimal prev = 1;
                decimal curr = 1;
                byte i = 0;
                yield return prev;
                yield return curr;
                while (i < Quantity)
                {
                    decimal next = 0;
                    try {
                        next = prev + curr;
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                    yield return next;
                    prev = curr;
                    curr = next;
                    ++i;
                }
            }
        }
        static void Main1()
        {
            Console.Write("Введите количество: ");
            Quantity = byte.Parse(Console.ReadLine());
            foreach (var v in Fibonacci) Console.WriteLine(v);
            Console.ReadKey();
        }
    }
}
