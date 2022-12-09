using Amld.Extensions.Logging.DiskFile;
using System.Collections.Concurrent;

namespace Amld.Extensions.Logging
{
    public class Processor
    {
        private readonly Thread _outputThread;
        private readonly BlockingCollection<LogEntry> _messageQueue;
        private readonly ILoggerWriter loggerWriter;
        private readonly IFileWriter _fileWriter;
        public Processor(int maxQuene, ILoggerWriter loggerWriter, IFileWriter fileWriter)
        {
            _messageQueue = new BlockingCollection<LogEntry>(maxQuene);
            _outputThread = new Thread(Consumer)
            {
                IsBackground = true,
                Name = "Console logger queue processing thread"
            };
            _outputThread.Start();
            this.loggerWriter = loggerWriter;
            _fileWriter = fileWriter;
        }

        public void Enqueue(LogEntry log)
        {
            if (!_messageQueue.TryAdd(log))
            {
                _fileWriter.Writer(new FileEntry("添加日志失败!"));
            };
        }
        private void Consumer()
        {
            try
            {
                foreach (var log in _messageQueue.GetConsumingEnumerable())
                {

                    loggerWriter.Write(log);
                }
            }
            catch(Exception e)
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch{
                }
                _fileWriter.Writer(new FileEntry("消费日志队列失败!" + e.ToString()));
            }
        }
        public void Dispose()
        {
            try
            {
                _outputThread.Join(1500);
            }
            catch (ThreadStateException) { }

            _messageQueue.Dispose();
        }
    }
}
