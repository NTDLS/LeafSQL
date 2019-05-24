﻿using System;

namespace LeafSQL.Library.Payloads.Actions.Base
{
    public class ActionGenericObject : ActionGeneric
    {
        public ActionGenericObject(Guid sessionId)
            : base (sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// The containing schema.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// The name of the object
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// The Id of the object
        /// </summary>
        public Guid ObjectId { get; set; }
    }
}
