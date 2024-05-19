using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class InsertionSort
{
    public static T[] Sort<T>(T[] arr, SortMode mode) where T : IComparable<T>
    {
        int sortModeValue = mode == SortMode.Asc ? 1 : -1;
        int index = 1;
        int tempIndex;
        while (index < arr.Length)
        {
            //Console.WriteLine("Array (index: " + index + "): " + ArrayToString.Execute(arr));
            if (arr[index - 1].CompareTo( arr[index] ) == sortModeValue) //EDIT LATER
            {
                tempIndex = index;
                while (index >= 1)
                {
                    if (arr[index - 1].CompareTo(arr[index]) == -sortModeValue) break;
                    Swap.Execute(ref arr[index - 1], ref arr[index]);
                    index--;
                }
                index = tempIndex;
            }
            index++;
        }

        return arr;
    }
}
