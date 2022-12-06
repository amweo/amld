using Confluent.Kafka;

namespace Amld.Extensions.Logging.Kafka
{
    public interface IKafkaClient : IDisposable
    {
        /// <summary>
        /// 获取生产者
        /// </summary>
        /// <returns></returns>
        IProducer<Null, LogEntry> Producer();
    }
}