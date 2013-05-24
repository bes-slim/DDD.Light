﻿using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Realtor.API.Command.Realtor;
using DDD.Light.Realtor.REST.API.Bootstrap;
using DDD.Light.Realtor.REST.API.Resources;
using StructureMap;

namespace DDD.Light.Realtor.REST.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ConfigureMappings();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetUpIoC();
            MongoEventStore.MongoEventStore.Instance.Configure("mongodb://localhost", "DDD.Light.Realtor", "EventStore");
            EventBus.Instance.Configure(MongoEventStore.MongoEventStore.Instance);
            InitApp(MongoEventStore.MongoEventStore.Instance);
        }

        private static void InitApp(IEventStore eventStore)
        {
            HandlerSubscribtions.SubscribeAllHandlers(ObjectFactory.GetInstance);
            CreateRealtorIfNoneExist(eventStore);
        }

        private static void ConfigureMappings()
        {
            Mapper.CreateMap<RealtorListing, PostListing>()
                .ForMember(command => command.ListingId, mapper => mapper.MapFrom(resource => resource.Id));
        }

        private static void SetUpIoC()
        {
            var container = StructureMapConfig.ConfigureDependencies();
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }

        private static void CreateRealtorIfNoneExist(IEventStore eventStore)
        {
            //todo: add any valid guid string here
            var realtorId = Guid.Parse(" put guid here ");
            if (eventStore.GetById(realtorId) == null)
                CommandBus.Instance.Dispatch(new SetUpRealtor(realtorId));
        }
    }
}