using System;
using System.Collections.Generic;

class NumFactor
{
    private Dictionary<int,int> dict = new Dictionary<int,int>();
    public void Set(int prime, int value)
    {
        dict[prime] = value;
    }
    public int Get(int prime)
    {
        if (!dict.ContainsKey(prime)) return 0;
        return dict[prime];
    }

    public int[] GetPrimes()
    {
        return dict.Keys.ToArray();
    }
}
