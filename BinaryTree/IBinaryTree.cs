using System.ComponentModel;

namespace BinaryTrees {
    public interface IBinaryTree
    {
        void Insert(int i);
        bool Search(int i);
        bool Remove(int i);
        void Print();
    }
}
