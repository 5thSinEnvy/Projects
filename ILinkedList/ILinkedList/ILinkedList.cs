using System.Collections.Generic;

namespace ILinkedList
{
    public interface ILinkedList<T>
    {
        Node<T> Head { get; set; }
        Node<T> Tail { get; set; }
        int Count { get; }

        void AddToHead(T data);
        void AddToTail(T data);
        T RemoveFromHead();
        T RemoveFromTail();

        bool DeleteFromPosition(int position);
        bool Search(T data);
        Node<T> SearchForPosition(int position);
        void SwapHeadAndTail();

        bool Compare(ILinkedList<T> other);

        IEnumerator<T> GetEnumerator();

    }
}