using Amld.Extensions.Logging.DiskFile;
using Moq;

namespace Amld.Extensions.Logging.Test
{
    public class LoggerTest
    {
        [Fact]
        public void GetKeyPrefixesTest()
        {

            var name = "Business.Services.TextEncoderService";

            var loggerWriterMock = new Mock<ILoggerWriter>();
            var fileWriterMock = new Mock<IFileWriter>();
            var logger = new Logger(new Processor(100, loggerWriterMock.Object, fileWriterMock.Object),name, new LoggerOption());
           
            var listKeyPrefix = new List<string>
            {
                "Business.Services.TextEncoderService",
                "Business.Services",
                "Business",
                "Default",
            };

            var collectPrefixs = new List<string>();
            foreach (var keyPrefix in logger.GetKeyPrefix(name))
            {
                collectPrefixs.Add(keyPrefix);
            }


            Assert.Equal(listKeyPrefix.Count, collectPrefixs.Count);
            for (int i = 0; i < listKeyPrefix.Count; i++)
            {
                Assert.Equal(listKeyPrefix[i], collectPrefixs[i]);
            }
        }
    }
}
