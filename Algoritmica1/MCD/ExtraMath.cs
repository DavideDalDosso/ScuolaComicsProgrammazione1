using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;

class ExtraMath
{
    public static NumFactor Factorise(int n)
    {
        var fact = new NumFactor();
        var currPrime = new PrimeNum();
        var num = n;

        while(currPrime.Get() != num)
        {
            if(num % currPrime.Get() == 0)
            {
                var numberIncreased = fact.Get(currPrime.Get()) + 1;
                fact.Set( currPrime.Get(), numberIncreased );
                num /= currPrime.Get();
            }
            else
            {
                currPrime.Next();
            }
        }
        fact.Set(num, 1);

        return fact;
    }

    public static int MCD(int a, int b)
    {
        var factA = Factorise(a);
        var factB = Factorise(b);

        return MCD(factA, factB);
    }

    public static int MCD(NumFactor a, NumFactor b)
    {
        var result = 1;

        var primesA = a.GetPrimes();
        var primesB = b.GetPrimes();

        int currA;
        int currB;
        int indexA = 0;
        int indexB = 0;

        while (indexA < primesA.Length && indexB < primesB.Length)
        {
            currA = primesA[indexA];
            currB = primesB[indexB];

            if(currA == currB)
            {
                var min = Math.Min( a.Get(currA), b.Get(currB) );
                //Console.WriteLine("Match in " + currA + " with similarity of: " + min);
                result *= min * currA;
                indexA++;
                indexB++;
            }
            else
            {
                if( currA < currB )
                {
                    indexA++;
                }
                else
                {
                    indexB++;
                }
            }
        }

        return result;
    }

    public static int FindMCD(int dividendo1, int dividendo2) //Minestrinad Version - dies against big numbers lol
    {
        int max, min;

        if (dividendo1 < dividendo2)
        {
            max = dividendo2;
            min = dividendo1;
        }
        else
        {
            max = dividendo1;
            min = dividendo2;
        }

        if (max % min == 0)
        {
            return min;
        }

        for (int divisore = min / 2; divisore > 0; divisore--)
        {
            if (min % divisore == 0 && max % divisore == 0)
            {
                return divisore; // Found the greatest common divisor
            }
        }

        return 1; // If no common divisor is found, 1 is the minimum possible value
    }
}
