using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BinaryNode<T> where T : IComparable<T>
{
    public T value = default;
    public BinaryNode<T> left;
    public BinaryNode<T> right;
    public int imbalance = 0;
    public override string ToString()
    {
        return value.ToString()+"("+imbalance+")";
    }
}
