using Inventory.Storage;

namespace Inventory.Modules;

public class EventStoreRepository<T> : IRepository<T> where T : AggregateRoot, new() //shortcut you can do as you see fit with new()
{
    private readonly IEventStore _storage;

    public EventStoreRepository(IEventStore storage)
    {
        _storage = storage;
    }

    public void Save(AggregateRoot aggregate, int expectedVersion)
    {
        _storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
    }

    public T GetById(Guid id)
    {
        var aggregate = new T();//lots of ways to do this

        var events = _storage.GetEventsForAggregate(id);

        aggregate.LoadsFromHistory(events);

        return aggregate;
    }
}
