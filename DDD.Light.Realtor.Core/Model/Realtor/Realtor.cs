﻿using System;
using System.Collections.Generic;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Event.Realtor;

namespace DDD.Light.Realtor.Core.Domain.Model.Realtor
{
    // aggregate root
    public class Realtor : Entity
    {
        private List<Guid> _offerIds; 
        private List<Guid> _listingIds; 

        private Realtor()
        {  
        }

        public Realtor(Guid id) : base(id)
        { 
            PublishEvent(new RealtorWasSetUp(id));
        }

        // API
        public void NotifyThatOfferWasMade(Guid offerId)
        {
            PublishEvent(new RealtorNotifiedThatOfferWasMade(offerId));
        }

        public void PostListing(Guid listingId)
        {
            PublishEvent(new PostedListing(listingId));
        }

        // Apply Events
        private void ApplyEvent(PostedListing @event)
        {
            if (_listingIds == null)
                _listingIds = new List<Guid>();
            _listingIds.Add(@event.ListingId);
        }

        private void ApplyEvent(RealtorNotifiedThatOfferWasMade @event)
        {
            if (_offerIds == null)
                _offerIds = new List<Guid>();
            _offerIds.Add(@event.OfferId);
        }
    }
}