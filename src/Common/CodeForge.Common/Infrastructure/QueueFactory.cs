using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CodeForge.Common.Infrastructure;

public static class QueueFactory
{
    public static void SendMessage(string exchangeName, string exchangeType, string queueName, object obj)
    {

        var channel = CreateBasicConsumer().EnsureExchange(exchangeName, exchangeType).EnsureQueue(queueName, exchangeName).Model;

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
        channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: null, body: body);


    }

    public static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory() { HostName = AppConstants.RABBITMQ_HOST };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return new EventingBasicConsumer(channel);
    }

    public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, string exchangeName, string exchangeType = AppConstants.DEFAULT_EXCHANGE_TYPE)
    {
        consumer.Model.ExchangeDeclare(exchange: exchangeName, type: exchangeType, durable: false, autoDelete: false);

        return consumer;
    }

    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, string queueName, string exchangeName)
    {
        consumer.Model.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, null);

        consumer.Model.QueueBind(queue: queueName, exchange: exchangeName, routingKey: queueName);

        return consumer;
    }

    public static EventingBasicConsumer Receive<T>(this EventingBasicConsumer consumer, Action<T> action)
    {
        consumer.Received += (m, EventArgs) =>
        {
            var body = EventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var model = JsonSerializer.Deserialize<T>(message);

            action(model);

            consumer.Model.BasicAck(EventArgs.DeliveryTag, false);
        };

        return consumer;
    }

    public static EventingBasicConsumer startConsuming(this EventingBasicConsumer consumer, string queueName)
    {
        consumer.Model.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        return consumer;
    }

}
