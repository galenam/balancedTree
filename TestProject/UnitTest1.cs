using System;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestProject
{
    public class UnitTest1
    {
        // todo : check duplicates in code
        static List<Tuple<List<int>, string>> _sourceResult = new List<Tuple<List<int>, string>>
        {
            new Tuple<List<int>, string>(new List<int> {6, 2, 3, 4, 5, 8, 1, 7}, "5 3 2 1 4 7 6 8" ),
            new Tuple<List<int>, string>(new List<int> {5,9,11,4,1,7,12,20, 2, 8, 17, 15,3, 19,18, 14, 16  }, "9 5 2 1 4 3 7 8 17 12 11 15 14 16 19 18 20"),
            new Tuple<List<int>, string>(new List<int> {31, 47, 91, 16, 1, 77, 9, 19, 71, 98, 23, 89  }, "47 16 1 9 23 19 31 77 71 91 89 98"),
            new Tuple<List<int>, string>(new List<int> {48, 78, 87, 1, 83, 100, 24, 26, 80, 69, 50, 45, 80, 85, 100, 78, 99, 91, 57, 43, 55, 75, 22, 71, 72, 95, 1  }, "78 48 24 1 1 22 43 26 45 57 50 55 71 69 75 72 87 83 80 78 80 85 99 91 95 100 100"),
            // еще 3 набора данных на insert, далее реализация нереализованных методов : remove, search
        };

        [Test, TestCaseSource(nameof(_sourceResult))]
        public void Test1(Tuple<List<int>, string> data)
        {
            var bTree = new BinaryTree();
            var inserted = InsertFromIEnumerable(bTree, data.Item1);
            Assert.True(inserted);
            var resultTest = bTree.Print();
            Assert.AreEqual(resultTest, data.Item2);
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
