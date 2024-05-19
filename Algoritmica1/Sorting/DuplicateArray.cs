using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DuplicateArray
{
    public static T[] Execute<T>(T[] arr)
    {
        var result = new T[arr.Length];
        for(int i=0; i<arr.Length; i++)
        {
            result[i] = arr[i];
        }
        return result;
    }
}
