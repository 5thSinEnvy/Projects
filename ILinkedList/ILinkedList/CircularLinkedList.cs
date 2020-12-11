using System;
using System.Collections.Generic;
using System.Text;

namespace ILinkedList
{
    public class CircularLinkedList<T> : ILinkedList<T>
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
                Head.Next = Head.Prev = Tail.Next = Tail.Prev = tmp;
            }
            else
            {
                Head.Prev = tmp;
                tmp.Next = Head;
                Head = tmp;
                Head.Prev = Tail;
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
                Tail.Next = Head;
            }

            Count++;
        }

        public bool Compare(ILinkedList<T> other)
        {
            var result = true;
            var tmp = Head;
            var otherHead = other.Head;
            while (tmp.Next != Head)
            {
                if (Comparer<T>.Default.Compare(tmp.Data, otherHead.Data) != 0)
                    result = false;

                otherHead = otherHead.Next;
                tmp = tmp.Next;
            }

            if (Comparer<T>.Default.Compare(tmp.Data, otherHead.Data) != 0)
                result = false;

            return result;
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
            var tmp = Head;
            Node<T> previousNode = null;
            var index = 0;
            previousNode = tmp;
            tmp = tmp.Next;
            index++;
            while (index < position && tmp != Head)
            {
                previousNode = tmp;
                tmp = tmp.Next;
                index++;
            }
            if ((index - 1 == position && tmp == Head) || (tmp != Head && index - 1 < position))
            {
                if (tmp == Head)
                    Head = Head.Next;
                else if (tmp == Tail)
                    Tail = previousNode;
                else
                    previousNode.Next = tmp.Next;
                result = true;
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var tmp = Head;
            while (tmp.Next != Head)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }

            yield return tmp.Data;
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
                Head.Prev = Tail;
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
                Tail.Next = Head;
            }

            Count--;
            return tailData;
        }

        public bool Search(T data)
        {
            bool result = false;
            var tmp = Head;
            if (tmp == null)
                throw new Exception("Cannot search in an empty linked list");
            while (tmp.Next != Head)
            {
                if (Comparer<T>.Default.Compare(tmp.Data, data) == 0)
                {
                    result = true;
                }
                tmp = tmp.Next;
            }
            if (Comparer<T>.Default.Compare(tmp.Data, data) == 0)
            {
                result = true;
            }
            return result;
        }

        public Node<T> SearchForPosition(int position)
        {
            var tmp = Head;
            var index = 0;
            if (Head == null)
                throw new Exception("Cannot search in an empty linked list.");

            while (index < position && tmp.Next != Head)
            {
                tmp = tmp.Next;
                index++;
            }
            if (index < position)
                throw new IndexOutOfRangeException("Index out of Range");

            return tmp;
        }

        public void SwapHeadAndTail()
        {
            var headData = RemoveFromHead();
            var tailData = RemoveFromTail();

            AddToHead(tailData);
            AddToTail(headData);
        }
    }
}
