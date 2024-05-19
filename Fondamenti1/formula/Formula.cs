using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EserciziWeek00.formula
{
    internal class Formula
    {
        public static void Main(string[] args)
        {
            //Declarations
            int n1;
            int n2;
            int n3;
            int n4;

            //Query
            Console.WriteLine("Insert the first value: ");
            n1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Insert the second value: ");
            n2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Insert the third value: ");
            n3 = int.Parse(Console.ReadLine());

            Console.WriteLine("Insert the fourth value: ");
            n4 = int.Parse(Console.ReadLine());

            float result = (n1 + n2) * (n3 - n4);

            //Result Buffer Declaration
            string bufferedResult;

            //Result display
            bufferedResult = result == 42
            ?
            "The result does equal to 42 (" + result + ")"
            :
            "The result does NOT equal to 42 (" + result + ")";

            Console.WriteLine(bufferedResult);
        }
    }
}
