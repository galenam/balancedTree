using System.Collections.Generic;
using System.Linq;
using BinaryTrees;

namespace TestProject
{
    public static class TestBase
    {
        public static bool InsertFromIEnumerable(BinaryTree bTree, IEnumerable<int> values)
        {
            if (bTree == null || !values.Any()) return false;
            foreach (var value in values)
            {
                bTree.Insert(value);
            }

            return true;
        }
    }
}