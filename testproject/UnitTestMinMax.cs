using System;
using System.Collections.Generic;
using System.Linq;
using BinaryTrees;
using NUnit.Framework;

namespace TestProject
{
    public class UnitTestMinMax
    {
        static List<(List<int> Data, int Max, int Min)> _sourceResult = new List<(List<int> Data, int Max, int Min)>
        {
            (Data: new List<int> {6, 2, 3, 4, 5, 8, 1, 7},Max: 8, Min: 1 ),
            (Data: new List<int> {5,9,11,4,1,7,12,20, 2, 8, 17, 15,3, 19,18, 14, 16  }, Max: 20,Min:1),
            (Data: new List<int> {31, 47, 91, 16, 1, 77, 9, 19, 71, 98, 23, 89  },Max: 98,Min:1 ),
            (Data: new List<int> {48, 78, 87, 1, 83, 100, 24, 26, 80, 69, 50, 45, 80, 85, 100, 78, 99, 91, 57, 43, 55, 75, 22, 71, 72, 95, 1  }, Max: 100, Min:1),
            (Data: new List<int>{77, 90, 46, 29, 54, 2, 68, 21, 8, 25, 99, 78, 15, 74, 86, 95, 22, 19, 10, 41, 76, 6, 20, 93, 63, 24, 13, 81, 38, 35, 75, 42, 65, 57, 12, 66, 70, 59, 53, 18, 82, 85, 72, 44, 4, 60, 7, 79, 69, 61, 34, 51, 83, 100, 16, 52, 37, 48, 3, 9, 30, 89, 87, 91, 26, 28, 92, 62, 5, 80, 23, 88, 43, 40, 36, 50, 98, 14, 67, 56, 49, 17, 96, 55, 73, 94, 58, 31, 45, 84, 11, 33, 64, 39, 27, 32, 97, 71, 1, 47 }, Max: 100, Min:1)
        };

        private void TestInitiate((List<int> Data, int Max, int Min) source, Func<BinaryTree, int?> f, int expectedValue)
        {
            var bTree = new BinaryTree();
            var inserted = TestBase.InsertFromIEnumerable(bTree, source.Data);
            Assert.True(inserted);
            var value = f(bTree);
            Assert.AreEqual(value.HasValue, true);
            Assert.AreEqual(value.Value, expectedValue);
        }

        [Test, TestCaseSource(nameof(_sourceResult))]
        public void TestMax((List<int> Data, int Max, int Min) source)
        {
            TestInitiate(source, (BinaryTree => BinaryTree.Max()), source.Max);
        }

        [Test, TestCaseSource(nameof(_sourceResult))]
        public void TestMin((List<int> Data, int Max, int Min) source)
        {
            TestInitiate(source, (BinaryTree => BinaryTree.Min()), source.Min);
        }
    }
}
