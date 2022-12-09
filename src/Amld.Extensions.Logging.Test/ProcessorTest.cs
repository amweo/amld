using Amld.Extensions.Logging.DiskFile;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amld.Extensions.Logging.Test
{
    public class ProcessorTest
    {
        [Fact]
        public void EnqueueTest()
        {
            var loggerWriterMock = new Mock<ILoggerWriter>();
            var fileWriterMock = new Mock<IFileWriter>();
            var proc = new Processor(5000, loggerWriterMock.Object, fileWriterMock.Object);
            proc.Enqueue(new LogEntry
            {
                AppId = "Amld.Extensions"

            });

        }
    }
}
