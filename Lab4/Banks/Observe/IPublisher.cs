namespace Banks.Observe;

public interface IPublisher
{
    void AddObserver(ISubscriber subscriber);
    void RemoveObserver(ISubscriber subscriber);
    void Inform();
}