namespace MyStack.Tests
{
    [TestFixture]
    public class EventTests
    {
        private MyStack<int> _stack;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _stack = new MyStack<int>(_fixture.Create<List<int>>());
        }

        [Test]
        public void MyStack_NewElementPushedEventTest()
        {
            bool isEventTriggered = false;
            int elementToPush = _fixture.Create<int>();

            _stack.ItemPushed += (_stack, item) => { isEventTriggered = true; };
            _stack.Push(elementToPush);

            Assert.That(isEventTriggered, Is.True);
        }

        [Test]
        public void MyStack_NewElementPushedAfterUnsubscribeEventTest()
        {
            bool isEventTriggered = false;
            int elementToPush = _fixture.Create<int>();
            void pushEventHanler(object? _stack, int item) { isEventTriggered = true; }

            _stack.ItemPushed += pushEventHanler;
            _stack.Push(elementToPush);
            _stack.Push(elementToPush);
            _stack.ItemPushed -= pushEventHanler;
            isEventTriggered = false;

            _stack.Push(elementToPush);

            Assert.That(isEventTriggered, Is.False);
        }

        [Test]
        public void MyStack_ClearedEventTest()
        {
            bool isEventTriggered = false;

            _stack.StackCleared += (_stack, args) => { isEventTriggered = true; };
            _stack.Clear();

            Assert.That(isEventTriggered, Is.True);
        }
    }
}
