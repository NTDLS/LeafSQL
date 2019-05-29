using System;
using System.Collections.Generic;

namespace LeafSQL.Engine.Interfaces
{
    public interface ICatalog<T>
    {
        Object LockObject { get; set; }

        List<T> Clone();
    }
}
