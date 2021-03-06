﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Interfaces
{
    public interface  IPayloadCompatible<Persist, Payload>
    {
        Persist Clone();
        Payload ToPayload();
    }
}
