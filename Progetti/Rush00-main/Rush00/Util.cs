using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Util
{
    /*public static int ReadInt(Action wrongFormatAction)
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
                wrongFormatAction?.Invoke();//This may not be implemented so null-check with '?.'
            }


        } while (invalid);

        return number;
    }*/ //Commented out just for archive reasons

    public static T ReadNumber<T>(Action wrongFormatAction) where T : INumber<T>, IConvertible//After some considerations, it's just easier to make generic number reading
    {
        T number = default(T);
        bool invalid;
        do
        {
            invalid = false;
            string input = Console.ReadLine();
            if (input == null) input = "";

            try
            {
                number = (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception ex)
            {
                invalid = true;
                wrongFormatAction?.Invoke();
            }


        } while (invalid);

        return number;
    }

    public static string ReadString()
    {
        string input = "";
        bool invalid;
        do
        {
            invalid = false;
            input = Console.ReadLine();//Reading input
            if (input == null) input = "";//Resolving null exception for compiler sake


        } while (invalid);

        return input;
    }

    public static string ArrayToString<T>(T[] array)
    {
        string buffer = "[";

        int i;

        for(i=0; i<array.Length-1; i++)
        {
            buffer += array[i].ToString() + ", " ;
        }

        buffer += array[i].ToString() + "]";

        return buffer;
    }
}
