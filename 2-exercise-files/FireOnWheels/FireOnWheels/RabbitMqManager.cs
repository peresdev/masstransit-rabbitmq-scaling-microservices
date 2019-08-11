using System;
using System.Text;
using FireOnWheels.Messaging;
using FireOnWheels.Notification.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FireOnWheels.Notification
{
    public class RabbitMqManager: IDisposable
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = RabbitMqConstants.RabbitMqUri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            connection.AutoClose = true;
        }

        public void ListenForOrderRegisteredEvent()
        {
            #region queue and qos setup
            channel.QueueDeclare(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue, 
                durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
#endregion
            var eventingConsumer = new EventingBasicConsumer(channel);
            eventingConsumer.Received += (chan, eventArgs) =>
            {
                var contentType = eventArgs.BasicProperties.ContentType;
                if (contentType != RabbitMqConstants.JsonMimeType)
                    throw new ArgumentException(
                        $"Can't handle content type {contentType}");

                var message = Encoding.UTF8.GetString(eventArgs.Body);
                var orderConsumer = new OrderRegisteredConsumer();
                var commandObj = 
                JsonConvert.DeserializeObject<OrderRegisteredEvent>(message);
                orderConsumer.Consume(commandObj);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, 
                    multiple: false);
            };
            channel.BasicConsume(
                queue: RabbitMqConstants.OrderRegisteredNotificationQueue,
                noAck: false,
                consumer: eventingConsumer);
        }

        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
