using Autofac;
using EventBus.Abstractions;
using Ordering.Api.Commands.Order;
using Ordering.Api.Queries;
using Ordering.Domain.AggregatesModel;
using Ordering.Infrastructure.EF.Repositories;
using System.Reflection;

namespace Ordering.Api.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new OrderQueries(QueriesConnectionString))
                .As<IOrderQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}