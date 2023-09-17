using System.Collections;

namespace MyStack.Implementation
{
    /// <summary>
    /// Linked list implementation of a generic stack.
    /// </summary>
    public class MyStack<T> : ICollection, IEnumerable<T>
    {
        private int _size;

        private MyLinkedListNode<T>? _head;

        /// <summary>
        /// Initializes a new instance of the MyStack class.
        /// </summary>
        public MyStack()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MyStack class with elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new stack.</param>
        /// <exception cref="ArgumentNullException">Thrown if the 'collection' parameter is null.</exception>
        public MyStack(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            foreach (T item in collection)
            {
                Push(item);
            }
        }

        /// <summary>
        /// Adds an item to the top of the stack.
        /// </summary>
        public void Push(T item)
        {
            var newNode = new MyLinkedListNode<T>(item);

            if (_head is null)
            {
                _head = newNode;
            }
            else
            {
                var currentHeadNode = _head;
                _head = newNode;
                newNode.Next = currentHeadNode;
                currentHeadNode.Prev = newNode;
            }
            _size++;
            ItemPushed?.Invoke(this, item);
        }

        /// <summary>
        /// Removes and returns the item at the top of the stack.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the stack size is 0.</exception>
        public T Pop()
        {
            if (_size is 0)
            {
                MyStack<T>.ThrowForEmptyStack();
            }

            T value = _head!.Data;
            _head = _head.Next;
            if (_head is not null)
            {
                _head.Prev = null;
            }

            _size--;

            return value;
        }

        /// <summary>
        /// Returns the item at the top of the stack without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the stack size is 0.</exception>
        public T Peek()
        {
            if (_size is 0)
            {
                MyStack<T>.ThrowForEmptyStack();
            }

            return _head!.Data;
        }

        /// <summary>
        /// Removes all Objects from the Stack.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _head = null;

            StackCleared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Checks, if the element exists in the stack
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var currentNode = _head;
            while (currentNode is not null)
            {
                if (currentNode.Data!.Equals(item))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        /// <summary>
        /// Returns the stack size.
        /// </summary>
        public int Count => _size;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        /// <summary>
        /// Event handler for when an item is pushed onto the stack
        /// </summary>
        public event EventHandler<T>? ItemPushed;

        /// <summary>
        /// Event handler for when the stack is cleared
        /// </summary>
        public event EventHandler? StackCleared;

        /// <summary>
        /// Copies the elements to an Array, starting at particular Array index
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void CopyTo(Array array, int index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            if (array.Length - index < _size)
            {
                throw new ArgumentException("The destination array has insufficient space.");
            }

            if (_size < 0)
            {
                MyStack<T>.ThrowForEmptyStack();
            }

            if (array is not T[] typedArray)
            {
                throw new ArgumentException("The destination array cannot be cast to type T[].", nameof(array));
            }

            var currentNode = _head;
            while (currentNode != null)
            {
                typedArray[index++] = currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        /// <summary>
        /// Copies the Stack to an array, in the same order Pop would return the items.
        /// </summary>
        /// <returns>Array type of <typeparamref name="T"/></returns>
        public T[] ToArray()
        {
            if (_size == 0)
            {
                return Array.Empty<T>();
            }

            T[] objArray = new T[_size];
            int i = 0;
            while (_size > 0)
            {
                objArray[i] = Pop();
                i++;
            }
            return objArray;
        }

        /// <returns>An enumerator that can be used to iterate through the elements in the stack.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static void ThrowForEmptyStack()
        {
            throw new InvalidOperationException("The stack is empty.");
        }
        /// <summary>
        /// Implementation of Enumerator for MyStack
        /// </summary>
        public sealed class MyEnumerator<T> : IEnumerator<T>
        {
            private readonly MyLinkedListNode<T>? _head;
            private MyLinkedListNode<T>? _currentNode;

            /// <summary>
            /// Initializes a new instance of the MyEnumerator class with the specified head node.
            /// </summary>
            /// <param name="headNode">The head node of the linked list.</param>
            public MyEnumerator(MyLinkedListNode<T>? headNode)
            {
                _head = headNode;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public T Current => _currentNode!.Data;

            object IEnumerator.Current => Current!;

            /// <summary>
            /// Advances the enumerator to the next element in the linked list.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (_currentNode is null && _head is not null)
                {
                    _currentNode = _head;
                    return true;
                }
                if (_currentNode?.Next is not null)
                {
                    _currentNode = _currentNode.Next;
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Resets the enumerator to its initial position, which is before the first element in the linked list.
            /// </summary>
            public void Reset()
            {
                _currentNode = null;
            }

            /// <summary>
            /// Disposes of any resources used by the enumerator (not used in this implementation).
            /// </summary>
            public void Dispose()
            {
                // Implementation for IDisposable.Dispose if needed.
            }
        }
    }
}