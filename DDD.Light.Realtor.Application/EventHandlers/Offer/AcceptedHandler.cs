﻿using DDD.Light.Messaging;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Realtor.Core.Domain.Model.Buyer.AggregateRoot.Contract;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Offer
{
    public class AcceptedHandler : EventHandler<Core.Domain.Events.Offer.Accepted>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public AcceptedHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(Core.Domain.Events.Offer.Accepted @event)
        {
            var buyer = _buyerRepo.GetById(@event.Offer.BuyerId);
            buyer.NotifyOfRejectedOffer(@event.Offer);

        }
    }
}