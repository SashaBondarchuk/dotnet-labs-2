namespace MyStack.Implementation
{
    public sealed class MyLinkedListNode<T>
    {
        public T Data { get; internal set; }
        public MyLinkedListNode(T data)
        {
            Data = data;
        }

        public MyLinkedListNode<T>? Next { get; internal set; }
        public MyLinkedListNode<T>? Prev { get; internal set; }
    }
}
