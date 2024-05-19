using System;
using System.Collections.Generic;
using static Conditions00;

class Conditions03
{
    static void Main(string[] args)
    {
        //Just continuosly asking queries
        while(true)
        {
            QueryNumber();
        }
    }

    public static void QueryNumber()
    {
        //The flag dictating the loop of the DO WHILE
        bool invalid;
        //The outputs from the DO WHILE
        int baseNumber = 0;
        int factorial = 0;
        do
        {
            //Exception error message buffer
            string buffer = "";
            try
            {
                //Reset flag
                invalid = false;

                //Input
                Console.WriteLine("Insert the number to calculate the factorial");
                buffer = Console.ReadLine();
                //This may throw exception
                baseNumber = int.Parse(buffer);
                //Get our factorial from the static class
                factorial = FactorialCalculator.CalculateFactorial(baseNumber);
                //In our custom class -1 just means error instead of throwing a custom exception
                //I'm just too lazy to properly implement an exception
                if(factorial == -1)
                {
                    Console.WriteLine("It is not allowed to retrieve the Factorial of a negative number");
                    invalid = true;
                }
            } catch (FormatException e)
            { //Telling the user to not put invalid data
                invalid = true;
                Console.WriteLine("'" + buffer + "' is not a valid number");
            }
        } while (invalid);

        //Of course we'll be dealing with huge numbers and eventually we'll run out
        //of bits; I could use Long instead but won't fix the actual problem
        if(baseNumber >= 20) Console.WriteLine("WARNING number overflow may have occurred");
        //Output
        Console.WriteLine("The factorial of " + baseNumber + " is: " + factorial);
    }

    public static class FactorialCalculator
    {
        public static int CalculateFactorial(int baseNumber)
        {
            if (baseNumber < 0) return -1;

            //n is 1 because factorials always start out with multiplication of 1
            //0! = 1 cause it entirely skips the multiplication loop and
            //uses our conventional number
            //1! = 1 cause it's 1 * 1 duh
            int n = 1;
            for(int i = 1; i < baseNumber+1; i++)
            {
                n *= i;
            }
            return n;
        }
    }
}
