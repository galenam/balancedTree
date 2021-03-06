﻿using System;

namespace BinaryTrees
{
    /// <summary>
    /// https://www.niisi.ru/iont/projects/rfbr/90308/90308_miphi6.php - text description
    /// </summary>
    public class BinaryTree : IBinaryTree
    {
        public Node Root { get; set; }

        public void Insert(int i)
        {
            if (Root == null)
            {
                Root = new Node { Value = i, Height = 1 };
                return;
            }
            Root = Insert(Root, i);
        }

        public bool Search(int i)
        {
            return SearchInner(Root, i) != null;
        }
        private Node SearchInner(Node node, int i)
        {
            if (node == null) return null;
            if (node.Value == i) return node;
            var nodeChoose = node.Value <= i ? node.Right : node.Left;
            return SearchInner(nodeChoose, i);
        }

        public bool Remove(int i)
        {
            if (Root == null) return false;
            var searchedNode = SearchInner(Root, i);
            if (searchedNode == null) return false;
            var parent = searchedNode.Parent;

            // удаление листа
            if (searchedNode != null && searchedNode.Left == null && searchedNode.Right == null)
            {
                parent = ReplaceParent(parent, searchedNode.Value, null);
                parent = Rotate(parent);
                parent.Height = GetHeight(parent);

                parent.Parent = ReplaceParent(parent.Parent, parent.Value, parent);
                var parentParent = parent.Parent;
                var newHeight = GetHeight(parentParent);
                if (newHeight == parentParent.Height) return true;

                while (newHeight != parentParent.Height)
                {
                    parentParent.Height = newHeight;
                    if (parentParent.Parent == null) break;
                    parentParent = parentParent.Parent;
                    newHeight = GetHeight(parentParent);
                }
                return true;
            }

            if (searchedNode != null && searchedNode.Right == null)
            {
                parent = ReplaceParent(parent, searchedNode.Value, searchedNode.Left);
                parent = Rotate(parent);
                return true;
            }

            var minInRightSubTree = FindMinInLeftSubTree(searchedNode.Right);
            if (minInRightSubTree.Right != null && minInRightSubTree.Parent != null)
            {
                minInRightSubTree.Parent.Right = minInRightSubTree.Right;
                minInRightSubTree.Right.Parent = minInRightSubTree.Parent;
            }
            else if (minInRightSubTree.Right == null)
            {
                minInRightSubTree.Parent = ReplaceParent(minInRightSubTree.Parent, minInRightSubTree.Value, null);
            }

            searchedNode.Value = minInRightSubTree.Value;

            searchedNode = Rotate(searchedNode);
            minInRightSubTree = null;
            if (parent == null)
            {
                Root = searchedNode;
                return true;
            }
            parent = ReplaceParent(parent, searchedNode.Value, searchedNode);
            parent.Height = GetHeight(parent);
            return true;
        }

        private Node ReplaceParent(Node parent, int valueToCompare, Node replacedNode)
        {
            if (parent == null)
            {
                return parent;
            }
            if (parent.Value <= valueToCompare)
            {
                parent.Right = replacedNode;
            }
            else
            {
                parent.Left = replacedNode;
            }
            return parent;
        }

        private Node FindMinInLeftSubTree(Node node)
        {
            if (node == null || node.Left == null) return node;
            return FindMinInLeftSubTree(node.Left);
        }

        public string Print()
        {
            if (Root == null) return null;
            return PrintInner(Root)?.Trim();
        }

        string PrintInner(Node node)
        {
            if (node == null) return string.Empty;
            var result = $"{node.Value} ";
            if (node.Left != null)
                result += PrintInner(node.Left);
            if (node.Right != null)
                result += PrintInner(node.Right);
            return result;
        }


        Node Insert(Node node, int i)
        {
            if (node == null)
                return null;
            if (node.Value <= i)
            {
                if (node.Right == null)
                {
                    node.Right = new Node { Value = i, Height = 1, Parent = node };
                    node.Height = GetHeight(node);
                    return node;
                }
                node.Right = Insert(node.Right, i);
                node.Height = GetHeight(node);
                return Rotate(node);
            }

            if (node.Left == null)
            {
                node.Left = new Node { Value = i, Height = 1, Parent = node };
                node.Height = GetHeight(node);
                return node;
            }
            node.Left = Insert(node.Left, i);
            node.Height = GetHeight(node);
            return Rotate(node);
        }

        int GetHeight(Node node)
        {
            var heightRight = node.Right == null ? 0 : GetHeight(node.Right);
            var heightLeft = node.Left == null ? 0 : GetHeight(node.Left);
            return Math.Max(heightLeft, heightRight) + 1;
        }

        int GetBalanceFactor(Node node) => node == null ? 0 : (node.Right?.Height ?? 0) - (node.Left?.Height ?? 0);

        Node Rotate(Node node)
        {
            if (node == null) return null;
            var balanceFactor = GetBalanceFactor(node);
            if (Math.Abs(balanceFactor) < 2) return node;
            if (balanceFactor == -2)
            {
                if (node.Left != null && GetBalanceFactor(node.Left) == -1)
                    return RotateLeftLeft(node);
                return RotateLeftRight(node);
            }

            if (node.Right != null && GetBalanceFactor(node.Right) == 1)
                return RotateRightRight(node);
            return RotateRightLeft(node);
        }

        /// <summary>
        ///                     C
        ///                    /
        ///                   B            ->                 B
        ///                  /                              /   \
        ///                 A                              A     C
        /// </summary>
        /// <param name="node"></param>
        Node RotateLeftLeft(Node node)
        {
            if (node == null) return null;
            var lostValue = node.Left.Right;
            node.Left.Right = node;
            node.Left.Parent = node.Parent;
            node.Parent = node.Left;
            node = node.Left;
            node.Right.Left = lostValue;
            node.Right.Height = GetHeight(node.Right);
            node.Left.Height = GetHeight(node.Left);
            node.Height = GetHeight(node);
            return node;
        }

        /// <summary>
        /// A
        ///   \
        ///    B            ->                  B
        ///     \                              / \
        ///      C                            A   C
        /// </summary>
        /// <param name="node"></param>
        Node RotateRightRight(Node node)
        {
            if (node == null)
                return null;
            var lostValue = node.Right.Left;
            node.Right.Left = node;
            node.Right.Parent = node.Parent;
            node = node.Right;
            node.Left.Right = lostValue;
            if (node.Left.Right != null)
            {
                node.Left.Right.Parent = node.Left;
            }

            node.Right.Parent = node;
            node.Left.Parent = node;

            node.Right.Height = GetHeight(node.Right);
            node.Left.Height = GetHeight(node.Left);
            node.Height = GetHeight(node);
            return node;
        }

        /// <summary>
        ///      C                              C
        ///     /                              /
        ///    A            ->                B            ->           B
        ///     \                            /                         / \
        ///      B                          A                         A   C
        /// </summary>
        /// <param name="node"></param>
        Node RotateLeftRight(Node node)
        {
            var lostValue = node.Left.Right.Left;


            node.Left.Right.Left = node.Left;
            node.Left.Right.Parent = node.Left.Parent;
            node.Left.Parent = node.Left.Right;
            node.Left = node.Left.Right;
            node.Left.Left.Right = lostValue;
            node.Left.Left.Height = GetHeight(node.Left.Left);
            node.Left.Height = GetHeight(node.Left);
            return RotateLeftLeft(node);
        }

        /// <summary>
        ///   A                             A
        ///    \                             \
        ///     C            ->               B            ->               B
        ///     /                              \                           / \
        ///    B                                C                         A   C
        /// </summary>
        /// <param name="node"></param>
        Node RotateRightLeft(Node node)
        {
            if (node == null) { return null; }
            var lostValue = node.Right.Left.Right;
            node.Right.Left.Right = node.Right;
            node.Right.Left.Parent = node.Right.Parent;
            node.Right = node.Right.Left;
            node.Right.Right.Left = lostValue;
            node.Right.Right.Parent = node.Right;
            node.Right.Right.Height = GetHeight(node.Right.Right);
            node.Right.Height = GetHeight(node.Right);
            return RotateRightRight(node);
        }

        private int? MinMax(Predicate<Node> condition, Func<Node, Node> func)
        {
            if (Root == null) { return null; }
            var node = Root;
            while (node != null)
            {
                if (condition(node))
                { node = func(node); }
                else
                { break; }
            }
            return node?.Value;

        }

        public int? Max()
        {
            return MinMax(node => node.Right != null, node => node.Right);
        }

        public int? Min()
        {
            return MinMax(node => node.Left != null, node => node.Left);
        }
    }
}
