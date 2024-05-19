using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

class ProgramMCD
{
    public static void Main(String[] args)
    {
        //Test 1 A BIG NUMBERRR
        //var a = 69 * 1337 * 1234;
        //var b = 23 * 420 * 12345;

        //Test 2 A smol nmbr
        //var a = 420;
        //var b = 21 * 63;

        //Test 3 A moderately balanced number
        var a = 19 * 67 * 2 * 3 * 7;
        var b = 654322;

        var factA = ExtraMath.Factorise(a);

        Console.WriteLine("Factors of A (" + a + "):");
        foreach ( var n in factA.GetPrimes())
        {
            Console.WriteLine(n + " - " + factA.Get(n) + " times");
        }

        var factB = ExtraMath.Factorise(b);

        Console.WriteLine();
        Console.WriteLine("Factors of B (" + b + "):");
        foreach (var n in factB.GetPrimes())
        {
            Console.WriteLine(n + " - " + factB.Get(n) + " times");
        }

        int result = 0;

        Console.WriteLine();
        Console.WriteLine("Clock Started!");
        var start = DateTime.Now;
        ///START STUFF
        
        for(int i=0; i<100000; i++)
        {
            result = ExtraMath.MCD(a, b);
        }

        ///END STUFF
        var end = DateTime.Now;
        Console.WriteLine("Clock Ended!");

        Console.WriteLine();
        Console.WriteLine("The result is: " + result);

        var seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine();
        Console.WriteLine("That took " + seconds + " seconds");
        //Please not that the operation time may fluctuate depending on the OS scheduling time

        Console.WriteLine();
        Console.WriteLine("Clock Started!");
        start = DateTime.Now;
        ///START STUFF

        for (int i = 0; i < 100000; i++)
        {
            result = ExtraMath.FindMCD(a, b);
        }

        ///END STUFF
        end = DateTime.Now;
        Console.WriteLine("Clock Ended!");

        Console.WriteLine();
        Console.WriteLine("The result is: " + result);

        seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine();
        Console.WriteLine("That took " + seconds + " seconds");
    }
}