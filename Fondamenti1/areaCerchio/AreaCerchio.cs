using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EserciziWeek00.areaCerchio
{
    internal static class AreaCerchio
    {
        public static void Main(string[] args)
        {
            //Query
            Console.WriteLine("Insert the radius of the circle: ");
            float radius = float.Parse(Console.ReadLine());

            //Process; Since in the result display this variable will be displayed as it is and won't be used elsewhere,
            //This variable declaration is uneccessary and is probably going to get optimized by the compiler;
            float result = CalculateSurface(radius);

            //Result display
            Console.WriteLine("The circle will have the surface of: " + result );
        }

        public static float CalculateSurface(float radius)
        {
            //Invoking the method of Pow(Number, Power) is quite expensive to perform so Number * Number is a good way simplify
            //a square of a number.
            return MathF.PI * radius * radius;
        }
    }
}
