using System;
using FireOnWheels.Messaging;
using MassTransit;

namespace FireOnWheels.Registration.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, 
                    RabbitMqConstants.RegisterOrderServiceQueue, e =>
                {
                    e.Consumer<RegisterOrderCommandConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for Register order commands.. " +
                              "Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
