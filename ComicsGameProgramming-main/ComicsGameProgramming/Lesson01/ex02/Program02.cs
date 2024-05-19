using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program02
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Insert the length: ");//Prompt

        int length = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        Console.WriteLine("Insert the height: ");//Prompt

        int height = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        string buffer = "";//Just a string buffer to avoid clogging the output stream for every single character
        string buffer2 = "";

        Console.WriteLine("\n");

        for (int i = 0; i < length; i++)
        {
            buffer += "*";
        }

        buffer += "\n";

        for (int i = 0; i < height; i++)
        {
            buffer2 += buffer;
        }

        Console.WriteLine(buffer2);//Output of this iteration
    }


}
