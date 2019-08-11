using FireOnWheels.Messaging;

namespace FireOnWheels.Registration.Service.Messages
{
    public class OrderRegisteredEvent: IOrderRegisteredEvent
    {
        private IRegisterOrderCommand command;
        private int orderId;
        public OrderRegisteredEvent(IRegisterOrderCommand command, int orderId)
        {
            this.command = command;
            this.orderId = orderId;
        }
        public int OrderId => orderId;
        public string PickupName => command.PickupName;
        public string PickupAddress => command.PickupAddress;
        public string PickupCity => command.PickupCity;

        public string DeliverName => command.DeliverName;
        public string DeliverAddress => command.DeliverAddress;
        public string DeliverCity => command.DeliverCity;

        public int Weight => command.Weight;
        public bool Fragile => command.Fragile;
        public bool Oversized => command.Oversized;
    }
}
