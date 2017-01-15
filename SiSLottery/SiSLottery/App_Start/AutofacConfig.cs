using Autofac;
using Autofac.Integration.WebApi;
using LotteryRepository;
using System.Reflection;
using System.Web.Http;
using Models;

namespace SiSLottery.App_Start
{
    public class AutofacConfig
    {
        private static IContainer _container;

        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterAssemblyTypes(Assembly.Load("Logger")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Models")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("Validation")).AsImplementedInterfaces();

            builder.RegisterType<LotteryRepository.LotteryRepository>().As<LotteryRepository.Interfaces.ILotteryRepository>().SingleInstance().AsImplementedInterfaces();

            _container = builder.Build();
            var config = GlobalConfiguration.Configuration;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
        }
    }
}