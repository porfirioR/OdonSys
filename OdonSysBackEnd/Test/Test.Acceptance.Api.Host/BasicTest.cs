using NUnit.Framework;

namespace AcceptanceTest.Host.Api
{
    internal class BasicTest : TestBase
    {
        [Test]
        public void BaseReturnOk()
        {
            // Just for run test base and insert script sql
            Assert.That(true, Is.True);
        }
    }
}
