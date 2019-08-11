using System;
using FireOnWheels.Messaging;
using FireOnWheels.Registration.Service.Messages;

namespace FireOnWheels.Registration.Service
{
    public class RegisterOrderConsumer
    {
        public void Consume(IRegisterOrderCommand command)
        {
            //Store order registration and get Id
            var id = 12;

            Console.WriteLine($"Order with id {id} registered");

            //notify subscribers that a order is registered
            var orderRegisteredEvent = 
                new OrderRegisteredEvent(command, id);
            //publish event
        }
    }
}
