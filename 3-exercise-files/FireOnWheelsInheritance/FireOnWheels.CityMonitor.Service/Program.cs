using System;
using FireOnWheels.Messaging;
using MassTransit;

namespace FireOnWheels.CityMonitor.Service
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "City monitor";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.EnablePerformanceCounters();
                cfg.ReceiveEndpoint(host, RabbitMqConstants.CityMonitorServiceQueue, e =>
                {
                    e.Consumer<CityMessageConsumer>();
                });
            });

            bus.Start();

            Console.WriteLine("Listening for city messages.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
