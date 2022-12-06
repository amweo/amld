using System.Collections.Concurrent;

namespace Amld.Extensions.Logging
{
    public class Processor
    {
        private readonly Thread _outputThread;
        private readonly BlockingCollection<LogEntry> _messageQueue;
        private readonly ILoggerWriter loggerWriter;

        public Processor(int maxQuene, ILoggerWriter loggerWriter)
        {
            _messageQueue = new BlockingCollection<LogEntry>(maxQuene);
            _outputThread = new Thread(Consumer)
            {
                IsBackground = true,
                Name = "Console logger queue processing thread"
            };
            _outputThread.Start();
            this.loggerWriter = loggerWriter;
        }

        public void Enqueue(LogEntry log)
        {
            if (!_messageQueue.TryAdd(log))
            {
#if DEBUG
                Console.WriteLine("添加日志失败");
#endif
            };
        }
        private void Consumer()
        {
            try
            {
                foreach (var log in _messageQueue.GetConsumingEnumerable())
                {

#if DEBUG
                    loggerWriter.Write(log);
#endif
                }
            }
            catch
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch{}
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
