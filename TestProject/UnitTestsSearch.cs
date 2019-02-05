using System;
using BinaryTrees;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestProject
{
    public class UnitTestSearch
    {
        static List<int> _sourceResult = new List<int> { 6, 2, 3, 4, 5, 8, 1, 7 };
        public BinaryTree BTree { get; } = new BinaryTree();

        [SetUp]
        public void Init()
        {
            if (BTree == null || !_sourceResult.Any()) return;
            foreach (var value in _sourceResult)
            {
                BTree.Insert(value);
            }
        }

        [TestCase(6, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(8, true)]
        [TestCase(1, true)]
        [TestCase(7, true)]
        [TestCase(17, false)]
        [TestCase(0, false)]
        [TestCase(-3, false)]
        public void TestSearch(int searchedValue, bool rightResult)
        {
            Assert.NotNull(BTree);
            var searchedResult = BTree.Search(searchedValue);
            Assert.AreEqual(searchedResult, rightResult);
        }
    }
}