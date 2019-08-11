namespace FireOnWheels.Messaging
{
    public interface IOrderRegisteredEvent: ICityMessage
    {
        int OrderId { get; }
        string PickupName { get; }
        string PickupAddress { get; }

        string DeliverName { get; }
        string DeliverAddress { get; }

        int Weight { get; }
        bool Fragile { get; }
        bool Oversized { get; }
    }
}
