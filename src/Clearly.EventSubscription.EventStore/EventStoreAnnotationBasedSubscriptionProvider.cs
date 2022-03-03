using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Clearly.Core.Interfaces;

namespace Clearly.EventSubscription.EventStore
{
    public class EventStoreAnnotationBasedSubscriptionProvider : ISubscriptionProvider
    {
        private readonly List<SubscriptionDetail> _subscriptionDetails = new List<SubscriptionDetail>();

        public void AddSubscriptionsFromAssembly(Assembly assembly)
        {
            var subscriptionTypes = GetSubscriptionTypes(assembly)
                .Where(x => _subscriptionDetails.All(y => y.Type != x.Type));

            _subscriptionDetails.AddRange(subscriptionTypes);
        }

        public IEnumerable<SubscriptionDetail> GetSubscriptionDetails()
        {
            return _subscriptionDetails;
        }

        private static IEnumerable<SubscriptionDetail> GetSubscriptionTypes(Assembly assembly)
        {
            return
                from type in assembly.DefinedTypes
                where type.IsClass
                let subscription = type.GetCustomAttribute<SubscriptionAttribute>()
                where subscription != null
                select new SubscriptionDetail
                {
                    Type = type,
                    EventHandlers = GetSubscriptionMethodMap(type),
                    Stream = new EventStoreEventStream
                    {
                        StreamName = subscription.StreamName
                    }
                };
        }

        private static IDictionary<Type, Func<object, IDomainEvent, Task>> GetSubscriptionMethodMap(TypeInfo type)
        {
            return GetSubscriptionMethods(type)
                .Where(x =>
                    !x.ContainsGenericParameters &&
                    x.GetParameters().Length == 1 &&
                    typeof(IDomainEvent).IsAssignableFrom(x.GetParameters().Single().ParameterType))
                .ToDictionary(x => x.GetParameters().Single().ParameterType, InvokeMethod);
        }

        private static Func<object, IDomainEvent, Task?> InvokeMethod(MethodInfo method)
        {
            return (obj, @event) => (Task?)method.Invoke(obj, new object[]{@event});
        }

        private static IEnumerable<MethodInfo> GetSubscriptionMethods(TypeInfo type)
        {
            return
                from method in type.DeclaredMethods
                let handler = method.GetCustomAttribute<HandlerAttribute>()
                where handler != null
                select method;
        }
    }
}