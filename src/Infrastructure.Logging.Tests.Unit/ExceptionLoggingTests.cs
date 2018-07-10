using System;
using Infrastructure.Logging.Extensions;
using NUnit.Framework;

namespace Infrastructure.Logging.Tests.Unit
{
    [TestFixture]
    internal class ExceptionLoggingTests
    {
        [Test]
        public void Test_Deep_Exception()
        {
            var firstException = new Exception("Exception (123)");

            var secondException = new Exception("Exception (456)", firstException);

            var thirdException = new Exception("Exception (789)", secondException);

            var deepMessage = thirdException.DeepException();

            Assert.IsTrue(deepMessage.Contains("Exception (123)"));

            Assert.IsTrue(deepMessage.Contains("Exception (456)"));
        }
    }
}
