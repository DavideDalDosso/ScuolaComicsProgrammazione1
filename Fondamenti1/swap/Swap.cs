using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EserciziWeek00.Swap
{
    //Static because it's not meant to be instantiated
    internal static class Swap
    {
        public static void Main(string[] args)
        {
            //Declarations
            float n1;
            float n2;

            //Query
            Console.WriteLine("Insert the first value (VAR1): ");
            n1 = float.Parse( Console.ReadLine() );

            Console.WriteLine("Insert the second value (VAR2): ");
            n2 = float.Parse(Console.ReadLine());

            //Process
            SwapVar<float>(ref n1, ref n2);

            //Result display
            Console.WriteLine("Value in VAR1: " + n1);
            Console.WriteLine("Value in VAR2: " + n2);
        }

        //Remember to pass the parameters as reference instead by
        //value or the swap not going to be reflected outside the method
        public static void SwapVar<T>(ref T leftVar, ref T rightVar)
        {
            T temp = leftVar;
            leftVar = rightVar;
            rightVar = temp;
        }
    }
}
