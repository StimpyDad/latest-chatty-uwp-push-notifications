using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serilog;
using SNPN.Common;
using SNPN.Data;
using SNPN.Monitor;
using System;
using System.IO;

namespace SNPN
{
	public class AppModuleBuilder
	{
		private static Lazy<IContainer> container = new Lazy<IContainer>(() => BuildContainer());
		public static IContainer Container { get { return container.Value; } }
		private static IContainer BuildContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<NotificationService>().As<INotificationService>().SingleInstance();
			builder.RegisterType<AccessTokenManager>().SingleInstance();
			builder.RegisterType<Monitor.Monitor>().SingleInstance();
			builder.Register(x =>
			{
				var configBuilder = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.SetBasePath(Directory.GetCurrentDirectory());

				return configBuilder.Build();
			}).SingleInstance();
			builder.Register(x =>
			{
				var config = x.Resolve<IConfigurationRoot>();
				var appConfig = new AppConfiguration();
				ConfigurationBinder.Bind(config, appConfig);
				return appConfig;
			}).SingleInstance();
			builder.Register<ILogger>(x =>
			{
				var config = x.Resolve<IConfigurationRoot>();
				return new LoggerConfiguration()
					.ReadFrom.Configuration(config)
					.CreateLogger();
			});
			builder.Register(x => new MemoryCache(new MemoryCacheOptions())).SingleInstance();
			builder.RegisterType<UserRepo>().As<IUserRepo>().InstancePerDependency();
			builder.RegisterType<NewEventHandler>().InstancePerDependency();
			builder.Register<Func<NewEventHandler>>(c =>
			{
				var ctx = c.Resolve<IComponentContext>();
				return () => ctx.Resolve<NewEventHandler>();
			});
			return builder.Build();
		}
	}
}
