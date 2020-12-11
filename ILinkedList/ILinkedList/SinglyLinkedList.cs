using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ILinkedList
{
    public class SinglyLinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public int Count { get; private set; }

        public void AddToHead(T data)
        {
            var node = new Node<T>(data);
            if (Head == null)
                Head = Tail = node;
            else
            {
                node.Next = Head;
                Head = node;
            }
            Count++;
        }

        public void AddToTail(T data)
        {
            var node = new Node<T>(data);
            if (Head == null)
                Head = Tail = node;
            else
            {
                var tailNode = Tail;
                tailNode.Next = node;
                Tail = node;
            }

            Count++;
        }

        public bool Compare(ILinkedList<T> other)
        {
            var tmp = Head;
            var otherHead = other.Head;
            while (tmp != null)
            {
                if (Comparer<T>.Default.Compare(tmp.Data, otherHead.Data) != 0)
                    return false;

                otherHead = otherHead.Next;
                tmp = tmp.Next;
            }

            return true;
        }

        public bool DeleteFromPosition(int position)
        {
            if (Head == null)
                throw new Exception("Cannot remove from an empty linked list.");
            if (position == 0 && Head == Tail)
            {
                Head = Tail = null;
                return true;
            }
            var result = false;
            var node = Head;
            Node<T> previousNode = null;
            var index = 0;
            while (index < position && node != null)
            {
                previousNode = node;
                node = node.Next;
                index++;
            }
            if (node != null)
            {
                if (node == Head)
                    Head = Head.Next;
                else if (node == Tail)
                    Tail = previousNode;
                else
                    previousNode.Next = node.Next;
                result = true;
                Count--;
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var tmp = Head;
            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
        }

        public T RemoveFromHead()
        {
            var data = Head.Data;
            if (Head == null)
                throw new Exception("Cannot remove from an empty linked list.");
            if (Head == Tail)
                Head = Tail = null;
            else
                Head = Head.Next;

            Count--;
            return data;
        }

        public T RemoveFromTail()
        {
            var node = Head;
            var data = Tail.Data;
            if (Head == null)
                throw new Exception("Cannot remove from an empty linked list.");
            if (Head == Tail)
                Head = Tail = null;
            else
            {
                while (node.Next != Tail)
                {
                    node = node.Next;
                }

                Tail = node;
            }

            Count--;
            return data;
        }

        public bool Search(T data)
        {
            var node = Head;
            if (node == null)
                throw new Exception("Cannot search in an empty linked list");
            while (node != null)
            {
                if (Comparer<T>.Default.Compare(node.Data, data) == 0)
                {
                    return true;
                }
                node = node.Next;
            }

            return false;
        }

        public Node<T> SearchForPosition(int position)
        {
            var node = Head;
            var index = 0;
            if (Head == null)
                throw new Exception("Cannot search in an empty linked list.");

            while (index < position)
            {
                node = node.Next;
                if (node == null)
                {
                    throw new IndexOutOfRangeException("Index out of Range");
                }
                index++;
            }

            return node;
        }

        public void SwapHeadAndTail()
        {
            var headData = RemoveFromHead();
            var tailData = RemoveFromTail();

            AddToHead(tailData);
            AddToTail(headData);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var tmp = Head;
            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
        }
    }
}
