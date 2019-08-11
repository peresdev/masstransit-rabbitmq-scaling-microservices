namespace FireOnWheels.Messaging
{
    public interface ICityMessage
    {
        string PickupCity { get; }
        string DeliverCity { get; }
    }
}