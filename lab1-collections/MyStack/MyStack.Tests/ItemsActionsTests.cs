namespace MyStack.Tests
{
    [TestFixture]
    public class ItemsActionsTests
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
        public void MyStack_PopTillStackIsEmptyTest()
        {
            var index = _testDataList.Count - 1;
            while (_stack.Count > 0)
            {
                var poppedItem = _stack.Pop();
                Assert.That(poppedItem, Is.EqualTo(_testDataList[index]));
                index--;
            }

            Assert.That(_stack, Has.Count.EqualTo(0));
        }

        [Test]
        public void MyStack_PeekTest()
        {
            var expectedTopItem = _testDataList[^1];
            var topItem = _stack.Peek();

            Assert.Multiple(() =>
            {
                Assert.That(topItem, Is.EqualTo(expectedTopItem));
                Assert.That(_stack, Has.Count.EqualTo(_testDataList.Count));
            });
        }

        [Test]
        public void MyStack_ClearTest()
        {
            _stack.Clear();

            Assert.That(_stack, Has.Count.EqualTo(0));
        }

        [Test]
        public void MyStack_ContainsTest()
        {
            bool itemExists = _stack.Contains(_testDataList[0]);
            Assert.That(itemExists, Is.True);

            int notExistingNumber = -1;
            bool itemDoesNotExist = _stack.Contains(notExistingNumber);
            Assert.That(itemDoesNotExist, Is.False);
        }
    }
}
