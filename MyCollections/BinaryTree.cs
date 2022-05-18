using System;
using System.Collections;
using System.Collections.Generic;

namespace MyCollections
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        public Node<T>? Head;

        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public event Action<T>? OnItemAdded;
        public event Action<T>? OnItemSearched;
        public event Action? OnClear;

        public T Min
        {
            get
            {
                var node = Head;

                if (node == null)
                    throw new InvalidOperationException("Cant get minimum from the empty tree");

                while (node.Left != null)
                {
                    node = node.Left;
                }

                return node.Value;
            }
        }

        public T Max
        {
            get
            {
                var node = Head;

                if (node == null)
                    throw new InvalidOperationException("Cant get maximum from the empty tree");

                while (node.Right != null)
                {
                    node = node.Right;
                }

                return node.Value;
            }
        }

        public bool IsEmpty => Head == null;

        public void Add(T item)
        {
            if (Head == null)
                Head = new Node<T>(item);
            else
                Insert_Impl(new Node<T>(item), Head);
            
            Count++;
            OnItemAdded?.Invoke(item);
        }

        public void Clear()
        {
            if (Head != null)
                Clear(Head);

            Head = null;
            OnClear?.Invoke();
        }

        public bool Contains(T value)
        {
            if (Head == null)
                return false;

            OnItemSearched?.Invoke(value);
            return GetNode(value, Head) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var t in this)
            {
                array[arrayIndex] = t;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetNode(T value, out Node<T>? node)
        {
            node = null;
            if (Head == null)
                return false;

            node = GetNode(value, Head);
            OnItemSearched?.Invoke(value);
            return node != null;
        }

        public IEnumerable<T> DFS()
        {
            return DFS(Head);
        }

        public IEnumerable<T> DFS(Node<T>? head)
        {
            if(head == null)
                yield break;
            
            if (head.Left != null)
                foreach (var i in DFS(head.Left))
                    yield return i;

            if (head.Right != null)
                foreach (var i in DFS(head.Right))
                    yield return i;

            yield return head.Value;
        }

        public IEnumerable<T> BFS()
        {
            return BFS(Head);
        }

        public IEnumerable<T> BFS(Node<T>? head)
        {
            if(head == null)
                yield break;
            
            var toVisitQue = new Queue<Node<T>>();
            toVisitQue.Enqueue(head);

            while (toVisitQue.Count > 0)
            {
                var i = toVisitQue.Dequeue();
                yield return i.Value;

                if (i.Left != null)
                    toVisitQue.Enqueue(i.Left);

                if (i.Right != null)
                    toVisitQue.Enqueue(i.Right);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DFS().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Insert_Impl(Node<T> node, Node<T> head)
        {
            int compareResult = head.CompareTo(node);
            if (compareResult == 0)
                throw new ArgumentException($"Element {node.Value} is already in the binary tree");

            if (compareResult > 0)
            {
                var headLeft = head.Left;
                if (headLeft != null)
                {
                    Insert_Impl(node, headLeft);
                }
                else
                {
                    head.Left = node;
                    node.Parent = head;
                }
            }
            else
            {
                var headRight = head.Right;
                if (headRight != null)
                {
                    Insert_Impl(node, headRight);
                }
                else
                {
                    head.Right = node;
                    node.Parent = head;
                }
            }
        }

        private Node<T>? GetNode(T value, Node<T> head)
        {
            int compareValue = head.CompareTo(value);
            if (compareValue == 0)
            {
                return head;
            }

            if (compareValue > 0)
            {
                var headLeft = head.Left;
                if (headLeft != null)
                {
                    return GetNode(value, headLeft);
                }

                return null;
            }

            var headRight = head.Right;
            if (headRight != null)
            {
                return GetNode(value, headRight);
            }

            return null;
        }

        private void Clear(Node<T> node)
        {
            if (node.Left != null)
            {
                Clear(node.Left);
                node.Left = null;
            }

            if (node.Right != null)
            {
                Clear(node.Right);
                node.Right = null;
            }
        }

        public class Node<TNode> : IComparable<Node<TNode>>, IComparable<TNode> where TNode : IComparable<TNode>
        {
            public TNode Value { get; }

            public Node<TNode> Parent
            {
                get => _parent;
                set => _parent = value;
            }

            public Node<TNode>? Left
            {
                get => _left;
                set => _left = value;
            }

            public Node<TNode>? Right
            {
                get => _right;
                set => _right = value;
            }

            private Node<TNode> _parent;

            private Node<TNode>? _left;
            private Node<TNode>? _right;

            public Node(TNode value)
            {
                Value = value;
            }

            public int CompareTo(Node<TNode>? other)
            {
                if (other != null) return Value.CompareTo(other.Value);
                return 1;
            }

            public int CompareTo(TNode? value)
            {
                return Value.CompareTo(value);
            }
        }
    }
}