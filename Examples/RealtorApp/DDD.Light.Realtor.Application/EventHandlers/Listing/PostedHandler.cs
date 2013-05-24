﻿using DDD.Light.Messaging;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Listing
{
    public class PostedHandler : EventHandler<Core.Domain.Events.Listing.Posted>
    {
        private readonly IRepository<Core.Domain.Model.Realtor.AggregateRoot.Realtor> _realtorRepo;
        private readonly IRepository<Core.Domain.Model.Listing.AggregateRoot.Listing> _listingRepo;

        public PostedHandler(IRepository<Core.Domain.Model.Realtor.AggregateRoot.Realtor> realtorRepo, IRepository<Core.Domain.Model.Listing.AggregateRoot.Listing> listingRepo)
        {
            _realtorRepo = realtorRepo;
            _listingRepo = listingRepo;
        }

        public override void Handle(Core.Domain.Events.Listing.Posted @event)
        {
            _realtorRepo.Save(@event.Realtor);
            _listingRepo.Save(@event.Listing);
        }
    }
}