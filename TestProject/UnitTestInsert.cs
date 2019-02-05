using System;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestProject
{
    public class UnitTestInsert
    {
        static List<Tuple<List<int>, string>> _sourceResult = new List<Tuple<List<int>, string>>
        {
            new Tuple<List<int>, string>(new List<int> {6, 2, 3, 4, 5, 8, 1, 7}, "5 3 2 1 4 7 6 8" ),
            new Tuple<List<int>, string>(new List<int> {5,9,11,4,1,7,12,20, 2, 8, 17, 15,3, 19,18, 14, 16  }, "9 5 2 1 4 3 7 8 17 12 11 15 14 16 19 18 20"),
            new Tuple<List<int>, string>(new List<int> {31, 47, 91, 16, 1, 77, 9, 19, 71, 98, 23, 89  }, "47 16 1 9 23 19 31 77 71 91 89 98"),
            new Tuple<List<int>, string>(new List<int> {48, 78, 87, 1, 83, 100, 24, 26, 80, 69, 50, 45, 80, 85, 100, 78, 99, 91, 57, 43, 55, 75, 22, 71, 72, 95, 1  }, "78 48 24 1 1 22 43 26 45 57 50 55 71 69 75 72 87 83 80 78 80 85 99 91 95 100 100"),
            new Tuple<List<int>, string>(new List<int>{77, 90, 46, 29, 54, 2, 68, 21, 8, 25, 99, 78, 15, 74, 86, 95, 22, 19, 10, 41, 76, 6, 20, 93, 63, 24, 13, 81, 38, 35, 75, 42, 65, 57, 12, 66, 70, 59, 53, 18, 82, 85, 72, 44, 4, 60, 7, 79, 69, 61, 34, 51, 83, 100, 16, 52, 37, 48, 3, 9, 30, 89, 87, 91, 26, 28, 92, 62, 5, 80, 23, 88, 43, 40, 36, 50, 98, 14, 67, 56, 49, 17, 96, 55, 73, 94, 58, 31, 45, 84, 11, 33, 64, 39, 27, 32, 97, 71, 1, 47 }, "46 21 8 4 2 1 3 6 5 7 15 12 10 9 11 13 14 19 17 16 18 20 34 29 25 23 22 24 27 26 28 31 30 33 32 38 36 35 37 42 40 39 41 44 43 45 68 57 51 49 48 47 50 53 52 55 54 56 63 60 59 58 61 62 66 65 64 67 85 77 72 70 69 71 75 74 73 76 81 79 78 80 83 82 84 90 87 86 89 88 95 92 91 93 94 99 97 96 98 100")
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
