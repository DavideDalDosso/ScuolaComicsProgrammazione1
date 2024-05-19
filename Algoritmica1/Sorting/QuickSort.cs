using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class QuickSort
{
    public static T[] Sort<T>(T[] arr, SortMode mode) where T : IComparable<T>
    {
        return Sort(arr, mode, 0, arr.Length);
    }
    private static T[] Sort<T>(T[] arr, SortMode mode, int start, int bound) where T : IComparable<T>
    {
        if (bound - start < 2) return arr;
        int sortModeValue = mode == SortMode.Asc ? 1 : -1;

        int index = 0;
        int swapIndex = -1;

        T pivot = arr[bound-1];
        //Console.WriteLine("Pivot: " + pivot + " Start: " + start + " Bound: " + bound);
        while(index < bound-1)
        {
            //Console.WriteLine("Array (index: " + index + " / " + swapIndex + "): " + ArrayToString.Execute(arr));
            if (arr[index].CompareTo( pivot ) == -sortModeValue)
            {
                swapIndex++;
                Swap.Execute(ref arr[swapIndex], ref arr[index]);
            }
            index++;
        }
        swapIndex++;
        Swap.Execute(ref arr[swapIndex], ref arr[index]);

        Sort(arr, mode, start, swapIndex-1);
        Sort(arr, mode, swapIndex + 1,bound);

        return arr;
    }
}
