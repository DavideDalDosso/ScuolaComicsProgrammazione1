using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

    class Program03
{
    public static void Main(string[] args)
    {
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

        string resultMsg = FindIncreasingSequence(nNumbers, array) ? "Ordered!" : "Unordered...";//Pre-result message

        Console.WriteLine("The array is: " + resultMsg);//Result output
    }

    public static bool FindIncreasingSequence(int numbers, int[] sequence) //I would've preferred to automatically get the array bounds
    {
        int buffer = 0;//A buffer to keep track the current highest value read
        for (int i = 0; i < numbers; i++)//This may end prematurely if 'numbers' is smaller
        {
            if (sequence[i] < buffer) //It's ok to get a number equal to our stored one
            {
                return false; //We could even return the index where we detected an out of line number instead
            }
            buffer = sequence[i];//We only access it twice, not bad enough to make a whole variable declaration for it
        }
        return true;//If we reached this spot, it means it's all sorted increasingly
    }

    public static int ReadInt(Action wrongFormatAction)
    {
        int number = 0;
        bool invalid;
        do
        {
            invalid = false;
            string input = Console.ReadLine();//Reading input
            if (input == null) input = "";//Resolving null exception for compiler sake

            try
            {
                number = int.Parse(input);//Converting input to number
            }
            catch (Exception ex)
            {
                invalid = true;//Uh oh, no number can be read
                wrongFormatAction.Invoke();
            }


        } while (invalid);

        return number;
    }
}
