using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program01
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Insert the length: ");//Prompt

        int length = Util.ReadNumber<int>(() =>//Oh sure thing making this utility class was much better than do this every time
        {
            Console.WriteLine("Please insert a valid number");
        });

        string buffer = "";//Just a string buffer to avoid clogging the output stream for every single character
        string buffer2 = "";

        Console.WriteLine("\n");

        for (int i=0; i < length; i++)
        {
            
            buffer += '*';//Put one more character to the buffer for the iteration
            buffer2 += buffer+'\n';

        }

        Console.WriteLine(buffer2);//Output of this iteration
    }
}
