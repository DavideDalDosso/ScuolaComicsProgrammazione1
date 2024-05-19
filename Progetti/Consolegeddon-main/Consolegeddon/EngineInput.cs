using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EngineInput
{
    private List<ConsoleKeyInfo> bufferedKeys = new List<ConsoleKeyInfo>();
    private List<ConsoleKeyInfo> availableKeys = new List<ConsoleKeyInfo>();
    public void Register(ConsoleKeyInfo key)
    {
        bufferedKeys.Add(key);
    }
    public void UpdateAvailable()
    {
        availableKeys.Clear();
        availableKeys.AddRange(bufferedKeys);
        bufferedKeys.Clear();
    }
    public bool KeyAvailable(ConsoleKey character)
    {
        bool found = false;
        availableKeys.ForEach(key =>
        {
            if (key.Key == character)
            {
                found = true;
                return;
            }
        });
        return found;
    }
}
