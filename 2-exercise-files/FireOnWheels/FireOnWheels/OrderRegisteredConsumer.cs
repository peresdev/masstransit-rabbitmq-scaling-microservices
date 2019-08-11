using System;
using FireOnWheels.Messaging;
using FireOnWheels.Notification.Messages;

namespace FireOnWheels.Notification
{
    public class OrderRegisteredConsumer
    {
        public void Consume(IOrderRegisteredEvent registeredEvent)
        {
            //Send notification to user
            Console.WriteLine($"Customer notification sent: Order id {registeredEvent.OrderId} registered");
        }
    }
}
