﻿using System;

namespace Mc2.CrudTest.Infrastructure.DataBase.Events
{

    public class OutBoxEventItem
    {
        public long OutBoxEventItemId { get; set; }
        public Guid EventId { get; set; }
        public string HappenedByUserId { get; set; }
        public Guid ProfileId { get; set; }
        public string HappenedByIpAddress { get; set; }
        public DateTime HappenedOn { get; set; }
        public string AggregateName { get; set; }
        public string AggregateTypeName { get; set; }
        public string AggregateId { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public string EventPayload { get; set; }
        public bool IsProcessed { get; set; }
        public Guid? AggregateBusinessId { get; set; }
    }
}
