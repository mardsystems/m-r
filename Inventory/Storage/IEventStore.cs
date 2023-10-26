using Inventory.Bus;
using System;
using System.Collections.Generic;

namespace Inventory.Storage;

public interface IEventStore
{
    void SaveEvents(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);

    List<Event> GetEventsForAggregate(Guid aggregateId);
}
