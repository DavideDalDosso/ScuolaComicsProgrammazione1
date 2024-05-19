using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MergeSort
{
    public static T[] Sort<T>(T[] arr, SortMode mode) where T : IComparable<T>
    {
        //Console.WriteLine("Array: " + ArrayToString.Execute(arr));
        if (arr.Length == 1) return arr;
        int sortModeValue = mode == SortMode.Asc ? 1 : -1;

        var half = arr.Length / 2;
        var extra = arr.Length % 2 == 1 ? 1 : 0;

        var leftArr = new T[half + extra];
        for(var i = 0; i < half; i++)
        {
            leftArr[i] = arr[i];
        }
        if(extra > 0) leftArr[half] = arr[half];

        var rightArr = new T[half];
        for (var i = 0; i < half; i++)
        {
            rightArr[i] = arr[i + half + extra];
        }

        Sort(leftArr, mode);
        //Console.WriteLine("Array: " + ArrayToString.Execute(leftArr));
        Sort(rightArr, mode);
        //Console.WriteLine("Array: " + ArrayToString.Execute(rightArr));

        int leftIndex = 0;
        int rightIndex = 0;
        int index = 0;

        while (leftIndex < leftArr.Length && rightIndex < rightArr.Length)
        {
            if ( leftArr[leftIndex].CompareTo(rightArr[rightIndex]) == sortModeValue)
            {
                arr[index] = rightArr[rightIndex];
                index++;
                rightIndex++;
            } else
            {
                arr[index] = leftArr[leftIndex];
                index++;
                leftIndex++;
            }
        }
        while (leftIndex < leftArr.Length)
        {
            arr[index] = leftArr[leftIndex];
            index++;
            leftIndex++;
        }
        while (rightIndex < rightArr.Length)
        {
            arr[index] = rightArr[rightIndex];
            index++;
            rightIndex++;
        }

        return arr;
    }
}
