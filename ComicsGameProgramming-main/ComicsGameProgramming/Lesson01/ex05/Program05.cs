using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Program05
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Please put the value of A: ");//Prompt

        int a = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        Console.WriteLine("Please put the value of B: ");//Prompt

        float b = Util.ReadNumber<float>(() =>//Be aware that '1.5' will not give a correct float; put '1,5' instead
        {
            Console.WriteLine("Please insert a valid number");
        });

        int doubleA = CalculateDouble(a);

        Console.WriteLine("\n");

        Console.WriteLine("The double of A is: " + doubleA);//Double output

        float product = CalculateProduct<float>(a, b);

        Console.WriteLine("\n");

        Console.WriteLine("The product of A and B is: " + product);//Product output

        Console.WriteLine("\n");

        if (a == b)//Output of the comparison result
        {
            Console.WriteLine("Both A and B have equal number");
        } else
        {
            if(Max(a, b) == a) Console.WriteLine("A is greater than B");
            else Console.WriteLine("B is greater than A");
        }

        Console.WriteLine("\n");

        Console.WriteLine("Please insert how many numbers to put: ");//Prompt

        int nNumbers = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        int[] array = new int[nNumbers];

        Console.WriteLine("Please put as many numbers as requested before: ");//Prompt

        for (int i = 0; i < nNumbers; i++)
        {
            array[i] = Util.ReadNumber<int>(() =>
            {
                Console.WriteLine("Please insert a valid number");
            });
        }

        Console.WriteLine("\n");

        Console.WriteLine("Array : " + Util.ArrayToString(array)); //Quick output of the overall array

        Console.WriteLine("\n");

        int sum = CalculateSum(array);

        Console.WriteLine("The sum of all the array elements is: " + sum); //Sum output

        Console.WriteLine("\n");

        Console.WriteLine("Please put the value of the number to find: ");//Prompt

        int presentN = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        string presentResult = NumberPresent(array, presentN) ? "Present!" : "Missing!";//Getting a string wherever it is present or not

        Console.WriteLine("\n");

        Console.WriteLine("The number '" + presentN + "' is: " + presentResult); //Presence output
    }

    public static T CalculateDouble<T>(T number) where T : INumber<T>
    {
        return number * T.CreateChecked(2);//It would've been easier for this scenario to just sum with itself but this handles a more generic multiplication scenario
    }

    public static T CalculateProduct<T>(T a, T b) where T : INumber<T>//Used a generics to handle both ints and floats (and more) at once
    {
        return a * b;
    }

    public static T CalculateSum<T>(IEnumerable<T> array) where T : INumber<T>//IEnumerable lets me to do this with list too
    {
        T buffer = default(T);
        foreach (T n in array)
        {
            buffer += n;
        }

        return buffer;
    }

    public static T Max<T>(T a, T b) where T : INumber<T> //We pass the generic parameters with ease as long it's a number
    {
        if (a.CompareTo(b) >= 0) return a; else return b;//CompareTo gives -1 on smaller; 0 on equal; 1 on bigger
    }//WARNING: it must return one regardless if they're equal so it returns A by default

    public static bool NumberPresent<T>(IEnumerable<T> array, T number) where T: INumber<T>
    {
        foreach(T n in array)
        {
            if(n ==  number) return true;
        }
        return false;
    }
}
