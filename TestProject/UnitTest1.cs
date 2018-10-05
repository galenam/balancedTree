using System;
using Xunit;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;

namespace TestProject {
    public class UnitTest1 {
        // todo : check duplicates in code
        readonly List<Tuple<List<int>, string>> _sourceResult = new List<Tuple<List<int>, string>>
        {
            //new Tuple<List<int>, string>(new List<int> {6, 2, 3, 4, 5, 8, 1, 7}, "5 3 2 1 4 7 6 8" ),
            new Tuple<List<int>, string>(new List<int> {5,9,11,4,1,7,12,20, 2, 8, 17, 15,3, 19,18, 14, 16  }, "5 3 2 1 4 7 6 8" )
        };

        [Fact]
        public void Test1()
        {
            foreach (var data in _sourceResult)
            {
                var bTree = new BinaryTree();
                var values = data.Item1;
                var inserted = InsertFromIEnumerable(bTree, values);
                Assert.True(inserted);
                var resultTest = bTree.Print();
                Assert.Equal(resultTest, data.Item2);
            }
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
