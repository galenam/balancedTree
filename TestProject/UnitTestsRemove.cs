using System;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestProject
{
    public class UnitTestRemove
    {
        static List<int> _sourceResult = new List<int> { 6, 2, 3, 4, 5, 8, 1, 7 };
        public BinaryTree BTree { get; set; }

        [SetUp]
        public void Init()
        {
            BTree = new BinaryTree();
            foreach (var value in _sourceResult)
            {
                BTree.Insert(value);
            }
        }


        [TestCase(6, true, "5 3 2 1 4 7 8")]
        [TestCase(12, false, "5 3 2 1 4 7 6 8")]
        [TestCase(2, true, "5 3 1 4 7 6 8")]
        [TestCase(4, true, "5 2 1 3 7 6 8")]
        [TestCase(5, true, "6 3 2 1 4 7 8")]
        [TestCase(56, false, "5 3 2 1 4 7 6 8")]
        [TestCase(3, true, "5 2 1 4 7 6 8")]
        [TestCase(8, true, "5 3 2 1 4 7 6")]
        [TestCase(1, true, "5 3 2 4 7 6 8")]
        [TestCase(7, true, "5 3 2 1 4 8 6")]
        public void Test1(int removedValue, bool removingResult, string printedBTree)
        {
            // после рефакторинга отвалилось 7 тестов, откатить, проверить, особенно последнюю строку
            var result = BTree.Remove(removedValue);
            Assert.AreEqual(result, removingResult);
            Assert.AreEqual(BTree.Print(), printedBTree);
        }
    }
}