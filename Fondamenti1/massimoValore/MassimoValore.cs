using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EserciziWeek00.massimoValore
{
    internal class MassimoValore
    {
        public static void Main(string[] args)
        {
            //Declaration of a integer set to the maximum possible value it can represent
            int number = int.MaxValue;

            //Before: 01111111; The first bit is the number's sign so we're in the positive numbers
            Console.WriteLine("Our integer, before summation has value of: " + number);

            number ++;

            //After: 10000000; Our number has overflown into the range of negative numbers.
            //Do note that the negative numbers DO have one extra number available unlike positives one since '0'
            //is already represented by 00000000, so 10000000 would be a redundant to represent '0'.
            Console.WriteLine("Our integer, after summation has value of: " + number);
        }
    }
}
