using System;
using System.Collections.Generic;

class PrimeNum
{
    private List<int> prevPrimes = new List<int>();
    private int num = 2;

    public int Get()
    {
        return num;
    }

    public void Next()
    {
        prevPrimes.Add(num);

        if(num == 2)
        {
            num = 3;
            return;
        }
        bool retry = true;

        while(retry)
        {
            retry = false;

            num += 2;

            foreach(int n in prevPrimes)
            {
                if( num % n == 0)
                {
                    retry = true;
                    break;
                }
            }
        }
    }
}
