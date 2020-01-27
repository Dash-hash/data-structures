using System;
using System.Collections;

namespace DataStructures
{
    public class AVLTree<T> : IEnumerable where T : IComparable<T>
    {
        #region Inner Types
        public class Node
        {
            public T Value { get; internal set; }

            public Node Parent;
            public Node Left;
            public Node Right;

            public Node(T value) =>
                Value = value;

            public void SetParent(Node parent) =>
                Parent = parent;

            public void SetChild(Node child)
            {
                if (child.Value.CompareTo(Value) >= 0)
                    Right = child;
                else
                    Left = child;
            }
        }
        #endregion

        private Node _root;

        public Node Root() =>
            _root;

        public int Height() =>
            Height(_root);

        public void Remove(T value) =>
            Remove(ref _root, value);

        public void Add(T value)
        {
            if (_root == null)
                _root = new Node(value);
            else
                Add(ref _root, _root, value);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator GetEnumerator()
        {
            var nodes = new System.Collections.Generic.Stack<Node>();
            var current = _root;

            while (nodes.Count != 0 || current != null)
            {
                while (current != null)
                {
                    nodes.Push(current);
                    current = current.Left;
                }

                current = nodes.Pop();

                yield return current.Value;

                current = current.Right;
            }
        }

        private static int Height(Node node)
        {
            if (node == null)
                return 0;

            int leftHeight = 0;
            int rightHeight = 0;

            if (node.Left != null)
                leftHeight = Height(node.Left);
            if (node.Right != null)
                rightHeight = Height(node.Right);

            if (leftHeight > rightHeight)
                return leftHeight + 1;
            else
                return rightHeight + 1;
        }

        private void Add(ref Node node, Node parent, T value)
        {
            if (node == null)
                node = new Node(value)
                {
                    Parent = parent
                };
            else
            {
                if (value.CompareTo(node.Value) < 0)
                    Add(ref node.Left, node, value);
                else if (value.CompareTo(node.Value) >= 0)
                    Add(ref node.Right, node, value);
            }
            
            Balance(node);
        }

        private void Remove(ref Node node, T value)
        {
            if (node == null)
                return;

            if (value.CompareTo(node.Value) < 0)
                Remove(ref node.Left, value);

            else if (value.CompareTo(node.Value) > 0)
                Remove(ref node.Right, value);

            else if (value.CompareTo(node.Value) == 0)
            {
                Node temp;

                if (node.Left != null && node.Right == null)
                {
                    node.Left.SetParent(node.Parent);
                    node.Parent.SetChild(node.Left);
                }
                else if (node.Left == null && node.Right != null)
                {
                    node.Right.SetParent(node.Parent);
                    node.Parent.SetChild(node.Right);
                }
                else if (node.Left == null && node.Right == null)
                {
                    if (node.Value.CompareTo(node.Parent.Value) < 0)
                        node.Parent.Left = null;
                    else
                        node.Parent.Right= null;
                }
                else if (node.Left != null && node.Right != null)
                {
                    if (node.Right.Left != null)
                    {
                        temp = Leftmost(node.Right);
                        node.Value = temp.Value;
                        Remove(ref node.Right, temp.Value);
                    }
                    else
                    {
                        node.Right.Left = node.Left;
                        node.Left.Parent = node.Right;

                        if (node == _root)
                            _root = node.Right;
                        else
                        {
                            node.Right.SetParent(node.Parent);
                            node.Parent.SetChild(node.Right);
                        }
                    }
                }
            }

            Balance(node);
        }

        private void Balance(Node node)
        {
            if (node == null)
                return;

            if (Height(node.Right) - Height(node.Left) == 2)
            {
                if (Height(node.Right.Right) >= Height(node.Right.Left))
                    RotateLeft(node);
                else
                {
                    RotateRight(node.Right);
                    RotateLeft(node);
                }
            }
            else if (Height(node.Right) - Height(node.Left) == -2)
            {
                if (Height(node.Left.Right) <= Height(node.Left.Left))
                    RotateRight(node);
                else
                {
                    RotateLeft(node.Left);
                    RotateRight(node);
                }
            }
        }

        private void RotateRight(Node node)
        {
            var pivot = node.Left;

            if (node.Parent != null)
            {
                node.Left.SetParent(node.Parent);
                node.Parent.SetChild(node.Left);
                node.Parent = node.Left;
            }
            else
            {
                _root = pivot;
                node.Parent = _root;
                _root.Parent = null;
            }

            node.Left = pivot.Right;
            pivot.Right = node;
        }

        private void RotateLeft(Node node)
        {
            var pivot = node.Right;

            if (node.Parent != null)
            {
                node.Right.SetParent(node.Parent);
                node.Parent.SetChild(node.Right);
                node.Parent = node.Right;
            }
            if (node.Parent == null)
            {
                _root = pivot;
                node.Parent = _root;
                _root.Parent = null;
            }

            node.Right = pivot.Left;
            pivot.Left = node;
        }

        private Node Leftmost(Node node)
        {
            if (node.Left != null)
                return Leftmost(node.Left);

            return node;
        }
    }
}
