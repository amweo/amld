using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Amld.Extensions.Logging.Test
{
    public class LoggerProviderTest
    {


        [Fact]
        public void CreateLoggerTest()
        {
            var mock = new Mock<IOptionsMonitor<LoggerOption>>();
            mock.Setup(x => x.CurrentValue).Returns(new LoggerOption());
            //mock.Object.CurrentValue.LogLevel.Add("Default", LogLevel.Debug);
            //mock.Object.CurrentValue.LogLevel.Add("Microsoft", LogLevel.Warning);
            //mock.Object.CurrentValue.LogLevel.Add("System", LogLevel.Warning);

            var loggerWriterMock = new Mock<ILoggerWriter>();
            var loggerProvider = new LoggerProvider(mock.Object, loggerWriterMock.Object);

            var name = "Business.Services.TextEncoderService";
            var logger = loggerProvider.CreateLogger(name);
            Assert.NotNull(logger);
            var loggerTwo = loggerProvider.CreateLogger(name);
            Assert.Equal(logger, loggerTwo);
        }
    }
}