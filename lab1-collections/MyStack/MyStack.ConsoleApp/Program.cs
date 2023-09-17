using MyStack.Implementation;

namespace MyStack.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> ints = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var myStack = new MyStack<int>(ints);
            //add event handlers
            myStack.StackCleared += MyStack_StackCleared;
            myStack.ItemPushed += MyStack_ItemPushed;

            Console.WriteLine("Constuctor, enumerator tests. Foreach cycle: ");
            PrintCollection(myStack);

            myStack.Push(113);
            Console.WriteLine("\nPeek test: " + myStack.Peek());
            PrintCollection(myStack);

            Console.WriteLine("\nPop test: " + myStack.Pop());
            PrintCollection(myStack);

            var myEmptyStack = new MyStack<int>();
            myEmptyStack.Push(99);
            myEmptyStack.Push(88);
            myEmptyStack.Push(77);
            myEmptyStack.Push(66);

            Console.WriteLine("\nPushes to empty stack test: ");
            PrintCollection(myEmptyStack);

            Console.WriteLine("\nClear stack test");
            myEmptyStack.Clear();
            PrintCollection(myEmptyStack);

            Console.WriteLine("\nMy stack contains '8': " + myStack.Contains(8));

            Console.WriteLine("\nMy stack to array test: ");
            var stackArr = myStack.ToArray();
            Console.WriteLine("Type of arr: " + stackArr.GetType());
            PrintCollection(stackArr);

            Console.WriteLine("\nCopy stack to array test: ");
            var arrayToCopyIn = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var indexToStart = 2;

            myStack.Clear();
            myStack.Push(99);
            myStack.Push(88);
            myStack.Push(77);
            myStack.Push(66);
            myStack.CopyTo(arrayToCopyIn, indexToStart);

            Console.WriteLine("result array");
            PrintCollection(arrayToCopyIn);

            myStack.Clear();
        }

        private static void MyStack_ItemPushed(object? sender, int item)
        {
            Console.WriteLine("STACK GOT A NEW ITEM: " + item);
        }

        private static void MyStack_StackCleared(object? sender, EventArgs e)
        {
            Console.WriteLine("STACK IS CLEAR");
        }

        private static void PrintCollection<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}