using Community.CQRS.Abstractions;
using Community.Rabbit.Abstractions.Publisher;
using Community.Rabbit.Abstractions.Subscriber;
using Microsoft.Extensions.DependencyInjection;

namespace Community.Rabbit.Cqrs.EventSubscriber;

public static class Extensions
{
    public static IBusSubscriber SubscribeCommand<T>(this IBusSubscriber busSubscriber) where T : class, ICommand
        => busSubscriber.Subscribe<T>(async (serviceProvider, command, _) =>
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
        });

    public static IBusSubscriber SubscribeEvent<T>(this IBusSubscriber busSubscriber) where T : class, IEvent
        => busSubscriber.Subscribe<T>(async (serviceProvider, @event, _) =>
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IEventHandler<T>>().HandleAsync(@event);
        });
    
    public static Task SendAsync<TCommand>(this IBusPublisher busPublisher, TCommand command, object messageContext)
        where TCommand : class, ICommand
        => busPublisher.PublishAsync(command, messageContext: messageContext);

    public static Task PublishAsync<TEvent>(this IBusPublisher busPublisher, TEvent @event, object messageContext)
        where TEvent : class, IEvent
        => busPublisher.PublishAsync(@event, messageContext: messageContext);
}