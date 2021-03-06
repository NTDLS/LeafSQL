﻿using System;

namespace LeafSQL.Library.Payloads.Models
{
    public class Schema
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public Schema Clone()
        {
            return new Schema
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}
