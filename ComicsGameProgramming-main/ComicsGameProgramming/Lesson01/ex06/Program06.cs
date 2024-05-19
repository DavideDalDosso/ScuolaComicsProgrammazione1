using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class Program06
{
    public static void Main(string[] args)
    {

    }
    public static void Randomize<T>(ref IEnumerable<T> array) where T : INumber<T>
    {
        var random = new Random();
        for(int i=0; i < array.Count(); i++)
        {
            array[i] = random.Next(10); // TODO: Index this
        }
    }
}
