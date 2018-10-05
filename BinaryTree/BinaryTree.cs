using System;

namespace BinaryTrees {
    /// <summary>
    /// https://www.niisi.ru/iont/projects/rfbr/90308/90308_miphi6.php - text description
    /// </summary>
    public class BinaryTree : IBinaryTree {
        public Node Root { get; set; }

        public void Insert(int i)
        {
            if (Root == null)
            {
                Root = new Node {Value = i, Height = 1};
                return;
            }
            Insert(Root, i);
        }

        public bool Search(int i) => throw new NotImplementedException();

        public bool Remove(int i) => throw new NotImplementedException();

        public void Print()
        {

        }

        void Insert(Node node, int i) {
            if (node == null)
                return;
            if (node.Value <= i) {
                if (node.Right == null) {
                    node.Right = new Node { Value = i, Height = 1, Parent = node};
                    node.Height = GetHeight(node);
                    return;
                }
                Insert(node.Right, i);
                node.Height = GetHeight(node);
                node = Rotate(node);
                return;
            }

            if (node.Left == null) {
                node.Left = new Node { Value = i, Height = 1, Parent = node };
                node.Height = GetHeight(node);
                return;
            }
            Insert(node.Left, i);
            node.Height = GetHeight(node);
            node = Rotate(node);
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
            node.Left.Right = node;
            node.Left.Parent = node.Parent;
            node.Parent = node.Left;
            node = node.Left;
            node.Right.Left = null;
            node.Right.Height = GetHeight(node.Right);
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
            node.Right.Left = node;
            node.Right.Parent = node.Parent;
            node = node.Right;
            node.Right.Left = null;
            node.Right.Parent = node;
            node.Right.Height = GetHeight(node.Right);
            node.Height = GetHeight(node);
            return node.Right;
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
            node.Left.Right.Left = node.Left;
            node.Left.Right.Parent = node.Left.Parent;
            node.Left.Parent = node.Left.Right;
            node.Left = node.Left.Right;
            node.Left.Right = null;
            node.Left.Left.Right = null;
            //node.Left.Left.Parent = node.Left;
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
            node.Right.Left.Right = node.Right;
            node.Right.Left.Parent = node.Right.Parent;
            node.Right = node.Right.Left;
            node.Right.Right.Left = null;
            node.Right.Right.Parent = node.Right;
            node.Right.Right.Height = GetHeight(node.Right.Right);
            node.Right.Height = GetHeight(node.Right);
            return RotateLeftLeft(node);
        }
    }
}
