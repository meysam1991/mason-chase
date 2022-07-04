﻿using Mc2.CrudTest.Infrastructure.DataBase.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mc2.CrudTest.Infrastructure.DataBase.Events.OutBoxEventItemConfigurations
{
    public class BaseOutBoxEventItemEfCoreConfig : IEntityTypeConfiguration<OutBoxEventItem>
    {
        public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
        {
            builder.Property(c => c.HappenedByUserId).HasMaxLength(255);
            builder.Property(c => c.EventName).HasMaxLength(255);
            builder.Property(c => c.AggregateName).HasMaxLength(255);
            builder.Property(c => c.EventTypeName).HasMaxLength(500);
            builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
            builder.Property(c => c.ProfileId).HasMaxLength(64);
        }
    }
}
