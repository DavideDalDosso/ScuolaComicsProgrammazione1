using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EserciziWeek00.nomi
{
    internal class Nomi
    {
        public static void Main(string[] args)
        {
            //Creating the object.
            NameList list = new NameList();

            //Adding all the entries.
            list.Add("Marco");
            list.Add("Luca");
            list.Add("Giorgio");
            list.Add("Luca");
            list.Add("Giorgio");
            list.Add("Giorgio");
            list.Display();
            Console.WriteLine("-----");
            //Removing just one Giorgio and all the Luca.
            list.Remove("Giorgio");
            list.RemoveAll("Luca");
            list.Display();
            Console.WriteLine("-----");
            //Repeatedly removing way more Giorgios than we have.
            list.Remove("Giorgio");
            list.Remove("Giorgio");
            list.Remove("Giorgio");
            list.Remove("Giorgio");
            list.Remove("Giorgio");
            list.Display();
            Console.WriteLine("-----");
            //Adding just one Giorgio back.
            list.Add("Giorgio");
            list.Display();
        }
        
        public class NameList()
        {
            //Used a Dictionary because it's random access and allows me to flag those name/keys with certain properties.
            //such as a count of how many times insertion and deletion has been performed.
            private Dictionary<String, int> names = new Dictionary<String, int>();

            public void Add(String name)
            {
                //Adding the entry in the dictionary if it doesn't exist.
                if (!names.ContainsKey(name))
                {
                    names.Add(name, 0);
                };
                //Increment
                names[name]++;
            }

            public void Remove(String name)
            {
                //Does nothing if entry doesn't exist.
                if (!names.ContainsKey(name)) return;
                //Decrement and also cache it cause we don't want to access it twice.
                int temp = names[name] - 1;

                //If the count has reached 0, remove it else just decrement.
                if (names[name] == 0)
                    names.Remove(name);
                else
                    names[name] = temp;
            }
            public void Display()
            {
                foreach(var key in names.Keys)
                {
                    for(int i=0; i < names[key]; i++)
                    {
                        Console.WriteLine(key);
                    }
                }
            }
            public void RemoveAll(String name)
            {   //Does nothing if entry doesn't exist.
                if (!names.ContainsKey(name)) return;
                //Removes the entry regardless of how many times a name has been added.
                names.Remove(name);
            }
        }
    }
}
