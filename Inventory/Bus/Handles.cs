﻿namespace Inventory.Bus;

public interface Handles<T>
{
    void Handle(T message);
}
