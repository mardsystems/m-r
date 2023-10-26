using System;

namespace Inventory.Modules;

public interface IRepository<T> where T : AggregateRoot, new()
{
    void Save(AggregateRoot aggregate, int expectedVersion);
    T GetById(Guid id);
}
