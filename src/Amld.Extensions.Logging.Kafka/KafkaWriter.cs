using Amld.Extensions.Logging;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Amld.Extensions.Logging.Kafka
{
    public class KafkaWriter : ILoggerWriter, IDisposable
    {
        private readonly IKafkaClient _kafkaClient;

        private readonly IOptions<KafkaOption> _options;


        public KafkaWriter(IKafkaClient kafkaClient, IOptions<KafkaOption> options)
        {
            _kafkaClient = kafkaClient;
            _options = options;
        }
        //异步写入回调
        private void AsyncHandler(DeliveryReport<Null, LogEntry> deliveryReport)
        {
            if (deliveryReport.Error.IsError)
            {

            }
        }

        public void Write(LogEntry log)
        {
            var producer = _kafkaClient.Producer();
            //异步写入
            producer.Produce(
               _options.Value.Topic,
               new Message<Null, LogEntry>() { Value = log }, AsyncHandler);
        }

        public void Dispose()
        {
            _kafkaClient?.Dispose();
        }
    }
}
