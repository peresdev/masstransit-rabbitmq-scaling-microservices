using System;
using System.Threading.Tasks;
using FireOnWheels.Messaging;
using MassTransit;

namespace FireOnWheels.CityMonitor.Service
{
    public class CityMessageConsumer: IConsumer<ICityMessage>
    {
        public async Task Consume(ConsumeContext<ICityMessage> context)
        {
            await Console.Out.WriteLineAsync("City message captured");
        }
    }
}
