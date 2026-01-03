
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

        public EventBusClient()
        {
            connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "localhost",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            //QueueName = "queue-test";
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Publish<T>(T objectPublish, string queueName) where T : IntegrationEvent
        {
            try
            {
                var bodyMessage = Encoding.UTF8.GetBytes(objectPublish.ToString());
                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                _channel.BasicPublish(exchange: "", routingKey: queueName, body: bodyMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error publishing message", ex);
            }
        }

        //public void Subscribe<T>(T objectSubscribe) where T : IntegrationEvent
        public void Subscribe()
        {
            consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                Console.WriteLine($"Nova mensagem recebida: {DateTime.Now}");

                var stringMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                //var test = JsonSerializer.Deserialize<dynamic>(stringMessage);
            };
        }

        public void StartConsuming(string queueName)
        {
            _channel.BasicConsume(queueName, autoAck: true, consumer);
        }
    }
}
