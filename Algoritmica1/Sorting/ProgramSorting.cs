using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ProgramSorting
{
    public static void Main(String[] args)
    {
        var array = new int[]{5,6,9,1,2,8,7,3,420,1337,21,-1,13};
        //var array = new int[] { 37, 13 };

        Console.WriteLine("THE ARRAY");
        Console.WriteLine( ArrayToString.Execute(array) );
        Console.WriteLine();

        Console.WriteLine("BUBBLESORT GAMING");

        Console.WriteLine();
        Console.WriteLine("Clock Started!");

        var bubbleArray = DuplicateArray.Execute(array);

        var start = DateTime.Now;
        ///START STUFF

        BubbleSort.Sort( bubbleArray, SortMode.Desc );

        ///END STUFF
        var end = DateTime.Now;
        Console.WriteLine("Clock Ended!");
        Console.WriteLine();

        var seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine("BUBBLESORT GAMING: " + ArrayToString.Execute(bubbleArray) );
        Console.WriteLine("Time: " + seconds);

        Console.WriteLine();
        Console.WriteLine("INSERTIONSORT GAMING");

        Console.WriteLine();
        Console.WriteLine("Clock Started!");

        var insertionArray = DuplicateArray.Execute(array);

        start = DateTime.Now;
        ///START STUFF

        InsertionSort.Sort(insertionArray, SortMode.Desc);

        ///END STUFF
        end = DateTime.Now;
        Console.WriteLine("Clock Ended!");
        Console.WriteLine();

        seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine("INSERTIONSORT GAMING: " + ArrayToString.Execute(insertionArray));
        Console.WriteLine("Time: " + seconds);

        Console.WriteLine();
        Console.WriteLine("MERGESORT GAMING");

        Console.WriteLine();
        Console.WriteLine("Clock Started!");

        var mergeArray = DuplicateArray.Execute(array);

        start = DateTime.Now;
        ///START STUFF

        MergeSort.Sort(mergeArray, SortMode.Desc);

        ///END STUFF
        end = DateTime.Now;
        Console.WriteLine("Clock Ended!");
        Console.WriteLine();

        seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine("MERGESORT GAMING: " + ArrayToString.Execute(mergeArray));
        Console.WriteLine("Time: " + seconds);

        Console.WriteLine();
        Console.WriteLine("QUICKSORT GAMING");

        Console.WriteLine();
        Console.WriteLine("Clock Started!");

        var quickArray = DuplicateArray.Execute(array);

        start = DateTime.Now;
        ///START STUFF

        QuickSort.Sort(quickArray, SortMode.Desc);

        ///END STUFF
        end = DateTime.Now;
        Console.WriteLine("Clock Ended!");
        Console.WriteLine();

        seconds = (end.Ticks - start.Ticks) / 10000000f;

        Console.WriteLine("QUICKSORT GAMING: " + ArrayToString.Execute(quickArray));
        Console.WriteLine("Time: " + seconds);
    }
}
