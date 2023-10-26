using System;

namespace Inventory.Storage;

public class AggregateNotFoundException : Exception
{
}

public class ConcurrencyException : Exception
{
}
