
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BlockInfrastructure.EventBus
{
    public class EventBusClient : IEventBus
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IModel _channel;
        private string QueueName { get; set; }
        private EventingBasicConsumer consumer;

        public EventBusClient(string queueName)
        {
            connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            QueueName = queueName;
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(
                queue: QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        public void Publish<T>(T objectPublish) where T : IntegrationEvent
        {
            var bodyMessage = Encoding.UTF8.GetBytes(objectPublish.ToString());
            _channel.BasicPublish(exchange: "", routingKey: QueueName, body: bodyMessage);
        }

        public void Subscribe<T>(T objectSubscribe) where T : IntegrationEvent
        {
            consumer = new EventingBasicConsumer(_channel);

            //T objectReturn = { };

            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var stringMessage = Encoding.UTF8.GetString(body);
                T foundedObject = JsonSerializer.Deserialize<T>(stringMessage);
            };
        }

        public void StartConsuming()
        {
            _channel.BasicConsume(QueueName, autoAck: true, consumer);
        }
    }
}
