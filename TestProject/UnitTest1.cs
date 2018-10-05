using Xunit;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;

namespace TestProject {
    public class UnitTest1 {
        [Fact]
        public void Test1()
        {
            var bTree = new BinaryTree();
            var values = new List<int> {6, 2, 3, 4, 5, 8, 1, 7};
            var inserted = InsertFromIEnumerable(bTree, values);
            Assert.True(inserted);
        }

        bool InsertFromIEnumerable(BinaryTree bTree, IEnumerable<int> values)
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
