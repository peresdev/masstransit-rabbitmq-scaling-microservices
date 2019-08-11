using System;
using System.Text;
using FireOnWheels.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FireOnWheels.Registration.Service
{
    public class RabbitMqManager: IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = 
                new ConnectionFactory {Uri = RabbitMqConstants.RabbitMqUri};
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForRegisterOrderCommand()
        {
            channel.QueueDeclare(
                queue: RabbitMqConstants.RegisterOrderQueue, 
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, 
                global: false);

            var consumer = new RegisteredOrderCommandConsumer(this);
            channel.BasicConsume(
                queue: RabbitMqConstants.RegisterOrderQueue,
                noAck: false,
                consumer: consumer);
        }

        public void SendOrderRegisteredEvent(IOrderRegisteredEvent command)
        {
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.OrderRegisteredExchange, 
                type: ExchangeType.Fanout);
            channel.QueueDeclare(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue, 
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);
            channel.QueueBind(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue,
                exchange: RabbitMqConstants.OrderRegisteredExchange, 
                routingKey: "");

            var serializedCommand = JsonConvert.SerializeObject(command);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstants.OrderRegisteredExchange, 
                routingKey: "",
                basicProperties: messageProperties, 
                body: Encoding.UTF8.GetBytes(serializedCommand));
        }

        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
