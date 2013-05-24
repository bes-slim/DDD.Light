﻿using System;

namespace DDD.Light.Realtor.Core.Domain.Event.Realtor
{
    public class RealtorWasSetUp
    {
        public Guid RealtorId { get; private set; }

        public RealtorWasSetUp(Guid realtorId)
        {
            RealtorId = realtorId;
        }
    }
}