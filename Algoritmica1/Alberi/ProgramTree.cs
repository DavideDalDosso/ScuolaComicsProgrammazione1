using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ProgramTree
{
    public static void Main(string[] args)
    {
        BinaryTree<int> tree = new BinaryTree<int>();
        tree.Add(9);
        tree.Add(5);
        tree.Add(4);
        tree.Add(7);
        tree.Add(16);
        tree.Add(14);
        tree.Add(17);
        tree.Add(26);
        tree.Add(24);
        tree.Add(27);
        tree.Add(36);
        tree.Add(34);
        tree.Add(37);
        tree.Add(36);
        tree.Add(33);
        tree.Add(36);

        Console.Out.WriteLine(tree.ToString());
    }
}
