namespace MyStack.Tests
{
    [TestFixture]
    public class EmptyStackTests
    {
        private MyStack<int> _stack;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _stack = new MyStack<int>();
        }

        [Test]
        public void MyStack_PushWithCheckOrderTest()
        {
            var testData = _fixture.Create<List<int>>();
            foreach (var item in testData)
            {
                _stack.Push(item);
            }

            var index = testData.Count - 1;
            foreach (var item in _stack)
            {
                Assert.That(item, Is.EqualTo(testData[index]));
                index--;
            }

            Assert.That(_stack, Has.Count.EqualTo(testData.Count));
        }

        [Test]
        public void MyStack_PopEmptyStackTest()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Test]
        public void MyStack_PeekEmptyStackTest()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }

        [Test]
        public void MyStack_PushAndPopTest()
        {
            int randomInteger = _fixture.Create<int>();
            _stack.Push(randomInteger);

            var poppedItem = _stack.Pop();

            Assert.Multiple(() =>
            {
                Assert.That(poppedItem, Is.EqualTo(randomInteger));
                Assert.That(_stack, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void MyStack_CopyEmptyStackToArrayTest()
        {
            int startIndexPosition = 0;
            int arraySize = 5;

            int[] array = _fixture.CreateMany<int>(arraySize).ToArray();
            int[] arrayCopy = new int[arraySize];
            array.CopyTo(arrayCopy, startIndexPosition);

            _stack.CopyTo(array, startIndexPosition);

            for (int i = 0; i < array.Length; i++)
            {
                Assert.That(array[i], Is.EqualTo(arrayCopy[i]));
            }
        }

        [Test]
        public void MyStack_ToArrayEmptyStackTest()
        {
            var array = _stack.ToArray();

            Assert.That(array, Is.Empty);
        }
    }
}
