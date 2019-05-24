﻿using LeafSQL.Library.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Indexes
{
    public class PersistIndexAttribute
    {
        public string Name { get; set; }

        public static PersistIndexAttribute FromPayload(IndexAttribute indexAttribute)
        {
            return new PersistIndexAttribute()
            {
                Name = indexAttribute.Name
            };
        }

        public PersistIndexAttribute Clone()
        {
            return new PersistIndexAttribute()
            {
                Name = Name
            };
        }

        public Library.Payloads.IndexAttribute ToPayload()
        {
            return new IndexAttribute()
            {
                 Name = this.Name
            };
        }

    }
}
