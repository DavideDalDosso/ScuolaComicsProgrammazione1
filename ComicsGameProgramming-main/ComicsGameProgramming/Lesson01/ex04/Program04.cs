using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program04
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

        Console.WriteLine("Array before shift: " + Util.ArrayToString(array));//Array output for comparison later on

        Console.WriteLine("\n");

        Console.WriteLine("Please insert the shift amount (positive = RightShift; negative = LeftShift): ");//Prompt

        int shift = Util.ReadNumber<int>(() =>
        {
            Console.WriteLine("Please insert a valid number");
        });

        array = ShiftArray(array, shift);//Shifting our array by the requested amount, wherever left or right

        Console.WriteLine("\n");

        Console.WriteLine("Array after shift: " + Util.ArrayToString(array));//Result output
    }

    public static T[] ShiftArray<T>(T[] array, int shift)//Please use this one instead of the derived methods
    {
        if(shift >= 0) return RightShiftArray(array, shift);
        else return LeftShiftArray(array, -shift);//Don't forget to reverse the shift value
    }

    private static T[] RightShiftArray<T>(T[] array, int offset)
    {
        if (offset < 0) throw new Exception();//These specific methods shouldn't be visible in a actual project so no nitpicky errors to handle
        if (offset == 0) return array;//Why would anyone put an offset of 0...
        T[] bufferArray = new T[array.Length];//The result array to send as method result


        int normOffset = offset % array.Length;//To handle a shift much higher than the array lenght by simply doing a smaller shift
        int firstBufferIndex = array.Length - normOffset;//The index of the original array element to put as first in the result one

        for(int i=0; i<normOffset; i++)//We first put in the result all the elements after our 'firstBufferIndex' which are going back to the beginning
        {
            bufferArray[i] = array[firstBufferIndex+i];
        }

        for (int i = 0; i < array.Length - normOffset; i++)//Then we put all the elements from original beginning to the one before 'firstBufferIndex'
        {
            bufferArray[i + normOffset] = array[i];//Since we're shifting forward, we're looking one unit backwards
        }

        return bufferArray;
    }

    private static T[] LeftShiftArray<T>(T[] array, int offset)//Same deal but with some things reversed
    {
        if (offset < 0) throw new Exception();//We check for negative offset since it should be reversed back to positives
        if (offset == 0) return array;
        T[] bufferArray = new T[array.Length];

        int normOffset = offset % array.Length;
        int lastIndex = array.Length - 1;//I need to keep track many times the last element index
        int lastBufferIndex = normOffset;//This time we just tell which is the element going to be in the end of the result

        for (int i = 0; i < normOffset; i++)//We first put in the result all the elements after our 'firstBufferIndex' which are going back to the beginning
        {
            bufferArray[array.Length - normOffset + i] = array[i];//Then we put all the starting section elements in the back of the result
        }

        for (int i = 0; i < array.Length - normOffset; i++)//Then we put all the elements following the 'lastBufferIndex'
        {
            bufferArray[i] = array[i + offset];//Since we're shifting backwards, we're looking one unit forward
        }

        return bufferArray;
    }
}
