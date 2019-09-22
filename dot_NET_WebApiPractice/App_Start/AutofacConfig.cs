using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using dot_NET_WebApiPractice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace dot_NET_WebApiPractice.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var bldr = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            bldr.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(bldr);
            bldr.RegisterWebApiFilterProvider(config);
            bldr.RegisterWebApiModelBinderProvider();
            var container = bldr.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder bldr)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TeamMappingProfile());
                cfg.AddProfile(new PlayerMappingProfile());
            }
            );

            bldr.RegisterInstance(config.CreateMapper())
                .As<IMapper>()
                .SingleInstance();

            bldr.RegisterType<BasketballContext>()
              .InstancePerRequest();

            bldr.RegisterType<BasketballRepository>()
              .As<IBasketballRepository>()
              .InstancePerRequest();
        }
    }
}