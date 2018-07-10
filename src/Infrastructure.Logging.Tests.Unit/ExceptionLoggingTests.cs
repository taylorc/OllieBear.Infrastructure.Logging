using System;
using Infrastructure.Logging.Extensions;
using Xunit;

namespace Infrastructure.Logging.Tests.Unit
{
    public class ExceptionLoggingTests
    {
        [Fact]
        public void Test_Deep_Exception()
        {
            var firstException = new Exception("Exception (123)");

            var secondException = new Exception("Exception (456)", firstException);

            var thirdException = new Exception("Exception (789)", secondException);

            var deepMessage = thirdException.DeepException();

            Assert.Contains("Exception (123)", deepMessage);

            Assert.Contains("Exception (456)", deepMessage);
        }
    }
}
