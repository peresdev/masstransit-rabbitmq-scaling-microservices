namespace FireOnWheels.Messaging
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = 
            "amqp://guest:guest@localhost:5672/";
        public const string JsonMimeType = 
            "application/json";

        public const string RegisterOrderExchange = 
            "fireonwheels.registerorder.exchange";
        public const string RegisterOrderQueue = 
            "fireonwheels.registerorder.queue";

        public const string OrderRegisteredExchange = 
            "fireonwheels.orderregistered.exchange";
        public const string OrderRegisteredNotificationQueue = 
            "fireonwheels.orderregistered.notification.queue";
    }
}
