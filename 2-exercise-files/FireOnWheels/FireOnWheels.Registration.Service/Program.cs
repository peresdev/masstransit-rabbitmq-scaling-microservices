using System;

namespace FireOnWheels.Registration.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration service";
            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.ListenForRegisterOrderCommand();
                Console.WriteLine("Listening for RegisterOrderCommand..");
                Console.ReadKey();
            }
        }
    }
}
