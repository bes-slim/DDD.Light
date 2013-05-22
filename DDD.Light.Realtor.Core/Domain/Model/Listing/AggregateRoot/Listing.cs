﻿using System;
using System.Collections.Generic;
using DDD.Light.Messaging;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Events.Listing;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Core.Domain.Model.Listing.AggregateRoot
{
    // aggregate root
    public class Listing : Entity
    {
        public Listing()
        {
            Location = new Location();
            Description = new Description();
        }

        public Location Location { get; set; }
        public Description Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<Guid> Offers { get; set; }
        public bool Active { get; set; }

        public void Deactivate()
        {
            Active = false;
            EventBus.Instance.Publish(GetType(), Id, new Deactivated { Listing = this });
        }
    }
}