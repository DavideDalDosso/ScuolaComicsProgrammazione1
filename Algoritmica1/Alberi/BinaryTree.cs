using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BinaryTree<T> where T : IComparable<T>
{
    BinaryNode<T>? root;

    public void Add(T value)
    {
        var node = new BinaryNode<T>();
        node.value = value;
        Add(node, null, null);
    }

    private void Add(BinaryNode<T> node, BinaryNode<T>? parent, BinaryNode<T>? grandParent)
    {
        if(parent == null)
        {
            if (root == null)
            {
                root = node;
                return;
            }
            Add(node, root, null);
        }
        else
        {
            if (node.value.CompareTo(parent.value) == -1)
            {
                if (parent.left != null)
                {
                    Add(node, parent.left, parent);
                }
                else
                {
                    parent.left = node;
                }
                parent.imbalance = parent.imbalance - 1;
                if (parent.imbalance == -2)
                {
                    if (parent.left.imbalance == -1)
                    {
                        RightRotate(parent.left, parent, grandParent);
                    } else
                    {
                        LeftRightRotate(parent.left, parent, grandParent);
                    }
                }
            }
            else
            {
                if (parent.right != null)
                {
                    Add(node, parent.right, parent);
                }
                else
                {
                    parent.right = node;
                }
                parent.imbalance = parent.imbalance + 1;
                if (parent.imbalance == 2)
                {
                    if (parent.right.imbalance == 1)
                    {
                        LeftRotate(parent.right, parent, grandParent);
                    }
                    else
                    {
                        RightLeftRotate(parent.right, parent, grandParent);
                    }
                }
            }
        }
    }
    public override string ToString()
    {
        string result = "";

        if(root != null)
        {
            result += ToString(root);
        }

        return result;
    }

    private string ToString(BinaryNode<T> node)
    {
        string result = "";

        result += node.ToString();
        result += " ( ";

        if(node.left != null)
        {
            result += ToString(node.left);
        } else result += "NIL";
        if (node.right != null)
        {
            result += ToString(node.right);
        }
        else result += "NIL";

        result += " ) ";

        return result;
    }
    public void RightRotate(BinaryNode<T> node, BinaryNode<T> parent, BinaryNode<T>? grandparent)
    {
        parent.imbalance += 2;
        node.imbalance += 1;
        if (parent == root) root = node;
        var temp = node.right;
        parent.left = temp;
        node.right = parent;
        if(grandparent != null) grandparent.left = node;
    }
    public void LeftRightRotate(BinaryNode<T> node, BinaryNode<T> parent, BinaryNode<T>? grandparent)
    {
        node.imbalance -= 1;
        node.right.imbalance -= 1;
        var temp = node.right.left;
        parent.left = node.right;
        node.right.left = node;
        node.right = temp;
        RightRotate(parent.left, parent, grandparent);
    }
    public void LeftRotate(BinaryNode<T> node, BinaryNode<T> parent, BinaryNode<T>? grandparent)
    {
        parent.imbalance -= 2;
        node.imbalance -= 1;
        if (parent == root) root = node;
        var temp = node.left;
        parent.right = temp;
        node.left = parent;
        if (grandparent != null) grandparent.right = node;
    }
    public void RightLeftRotate(BinaryNode<T> node, BinaryNode<T> parent, BinaryNode<T>? grandparent)
    {
        node.imbalance += 1;
        node.left.imbalance += 1;
        var temp = node.left.right;
        parent.right = node.left;
        node.left.right = node;
        node.left = temp;
        LeftRotate(parent.right, parent, grandparent);
    }
}
