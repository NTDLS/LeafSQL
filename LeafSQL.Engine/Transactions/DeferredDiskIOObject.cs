﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Transactions
{
    public class DeferredDiskIOObject
    {
        public string LowerDiskPath { get; set; }
        public string DiskPath { get; set; }
        public Object Reference { get; set; }
        public long Hits { get; set; }
        public IOFormat DeferredFormat { get; set; }
    }
}
