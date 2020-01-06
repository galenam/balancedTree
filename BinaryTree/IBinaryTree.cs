using System.ComponentModel;

namespace BinaryTrees
{
    public interface IBinaryTree
    {
        void Insert(int i);
        bool Search(int i);
        bool Remove(int i);
        string Print();

        int? Max();
        int? Min();
    }
}
