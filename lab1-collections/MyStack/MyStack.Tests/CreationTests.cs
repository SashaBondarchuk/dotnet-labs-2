namespace MyStack.Tests
{
    [TestFixture]
    public class CreationTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void MyStack_Constructor_InitializationTest()
        {
            var intData = _fixture.Create<List<int>>();
            var stringData = _fixture.Create<List<string>>();

            var intStack = new MyStack<int>(intData);
            var stringStack = new MyStack<string>(stringData);

            Assert.Multiple(() =>
            {
                Assert.That(intStack, Has.Count.EqualTo(intData.Count));
                Assert.That(stringStack, Has.Count.EqualTo(stringData.Count));
            });
        }

        [Test]
        public void MyStack_Constructor_ArgumentNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new MyStack<object>(null));
        }

        [Test]
        public void MyStack_CreationOrderTest()
        {
            var testData = _fixture.Create<List<char>>();
            var stack = new MyStack<char>(testData);

            var index = testData.Count - 1;

            foreach (var item in stack)
            {
                Assert.That(item, Is.EqualTo(testData[index]));
                index--;
            }
        }
    }
}