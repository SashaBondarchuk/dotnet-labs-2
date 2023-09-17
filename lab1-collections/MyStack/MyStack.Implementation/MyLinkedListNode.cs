namespace MyStack.Implementation
{
    /// <summary>
    /// Implementation of linked list node element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MyLinkedListNode<T>
    {
        /// <summary>
        /// A value, that is stored in the specific node
        /// </summary>
        public T Data { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the MyLinkedListNode class.
        /// </summary>
        /// <param name="data"></param>
        public MyLinkedListNode(T data)
        {
            Data = data;
        }

        /// <summary>
        /// A reference to a next node
        /// </summary>
        public MyLinkedListNode<T>? Next { get; internal set; }

        /// <summary>
        /// A reference to a previous node
        /// </summary>
        public MyLinkedListNode<T>? Prev { get; internal set; }
    }
}
