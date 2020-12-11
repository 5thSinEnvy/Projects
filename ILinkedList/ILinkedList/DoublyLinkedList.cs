using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ILinkedList
{
    public class DoublyLinkedList<T> : ILinkedList<T>, IEnumerable<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }

        public int Count { get; private set; }

        public void AddToHead(T data)
        {
            var tmp = new Node<T>(data);

            if (Head == null)
            {
                Head = Tail = tmp;
            }
            else
            {
                Head.Prev = tmp;
                tmp.Next = Head;
                Head = tmp;
            }

            Count++;
        }

        public void AddToTail(T data)
        {
            var tmp = new Node<T>(data);

            if (Head == null) Head = Tail = tmp;
            else
            {
                Tail.Next = tmp;
                tmp.Prev = Tail;
                Tail = tmp;
            }
            Count++;
        }

        public T RemoveFromHead()
        {
            if (Head == null)
                throw new Exception("Cannot remove from an empty linked list.");

            var headData = Head.Data;
            if (Head == Tail)
            {

                Head = Tail = null;
            }
            else
            {
                Head = Head.Next;
                Head.Prev = null;
            }

            Count--;
            return headData;
        }

        public T RemoveFromTail()
        {
            if (Head == null)
                throw new Exception("Cannot remove from an empty linked list.");
            var tailData = Tail.Data;
            if (Head == Tail)
            {
                Head = Tail = null;
            }
            else
            {
                Tail = Tail.Prev;
                Tail.Next.Prev = null;
                Tail.Next = null;
            }

            Count--;
            return tailData;
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

        public bool Search(T data)
        {
            if (Head == null)
                throw new Exception("Cannot search in an empty linked list.");
            var tmp = Head;
            while (tmp != null)
            {
                if (Comparer<T>.Default.Compare(tmp.Data, data) == 0)
                    return true;
                tmp = tmp.Next;
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
                    throw new IndexOutOfRangeException("Index out of Range");

                index++;
            }

            return node;
        }

        public void SwapHeadAndTail()
        {
            if (Head == null)
                throw new Exception("Cannot swap in an empty linked list.");
            var headData = RemoveFromHead();
            var tailData = RemoveFromTail();

            AddToHead(tailData);
            AddToTail(headData);
        }
        public bool Compare(ILinkedList<T> other)
        {
            if (Head == null || other.Head == null)
                throw new Exception("Cannot compare an empty linked list.");
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
        public IEnumerator<T> GetEnumerator()
        {
            var tmp = Head;
            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
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
