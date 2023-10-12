namespace MyStack.Tests
{
    [TestFixture]
    public class ArrayOperationsTests
    {
        private Fixture _fixture;
        private MyStack<int> _stack;
        private List<int> _testDataList;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _testDataList = _fixture.Create<List<int>>();
            _stack = new MyStack<int>(_testDataList);
        }

        [Test]
        public void MyStack_ToArrayTest()
        {
            var array = _stack.ToArray();
            int index = 0;

            Assert.That(array, Has.Length.EqualTo(_stack.Count));

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[index++]));
            }
        }

        [Test]
        public void MyStack_CopyToArrayWithSameSizeTest()
        {
            int[] array = new int[_stack.Count];
            int startIndexPosition = 0;

            _stack.CopyTo(array, startIndexPosition);

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[startIndexPosition++]));
            }
        }

        [Test]
        public void MyStack_CopyToArrayWithBiggerSizeTest()
        {
            int arrayLengthOffset = 3;
            int startIndexPosition = 0;

            int[] array = new int[_stack.Count + arrayLengthOffset];
            _stack.CopyTo(array, startIndexPosition);

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[startIndexPosition++]));
            }
        }

        [Test]
        public void MyStack_CopyToFilledArrayTest()
        {
            int startIndexPosition = 0;

            int[] array = _fixture.CreateMany<int>(_stack.Count).ToArray();
            _stack.CopyTo(array, startIndexPosition);

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[startIndexPosition++]));
            }
        }

        [Test]
        public void MyStack_CopyToFilledArrayWithBiggerSizeTest()
        {
            int startIndexPosition = 0;
            int arrayLengthOffset = 3;

            int[] array = _fixture.CreateMany<int>(_stack.Count + arrayLengthOffset).ToArray();
            _stack.CopyTo(array, startIndexPosition);

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[startIndexPosition++]));
            }
        }

        [Test]
        public void MyStack_CopyToArrayWithBiggerSize_WithDifferentStartingIndexTest()
        {
            int arrayLengthOffset = 4;
            int startIndexPosition = 2;

            int[] array = new int[_stack.Count + arrayLengthOffset];
            _stack.CopyTo(array, startIndexPosition);

            foreach (int item in _stack)
            {
                Assert.That(item, Is.EqualTo(array[startIndexPosition++]));
            }
        }

        [Test]
        public void MyStack_CopyTo_OutOfRangeTest()
        {
            int[] array = new int[_stack.Count];
            int outOfRangeIndex = array.Length;

            Assert.Throws<ArgumentOutOfRangeException>(() => _stack.CopyTo(array, outOfRangeIndex));
        }

        [Test]
        public void MyStack_CopyTo_InsufficientSpaceTest()
        {
            int[] array = new int[_stack.Count - 1];

            Assert.Throws<ArgumentException>(() => _stack.CopyTo(array, 0));
        }

        [Test]
        public void MyStack_CopyTo_NullArrayTest()
        {
            int[] array = null!;

            Assert.Throws<ArgumentNullException>(() => _stack.CopyTo(array, 0));
        }

        [Test]
        public void CopyTo_NegativeSize_ThrowsException()
        {
            _stack.Clear();

            Assert.Throws<InvalidOperationException>(() =>
            {
                _stack.CopyTo(new int[_stack.Count], 0);
            });
        }

        [Test]
        public void CopyTo_InvalidArrayType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _stack.CopyTo(new object[_stack.Count], 0);
            });
        }
    }
}
