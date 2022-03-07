using System.Net;
using System.Reflection;
using AutoMapper;
using Clearly.Core;
using Clearly.Core.Interfaces;
using Clearly.EventRepository.EventStore;
using Clearly.EventRepository.EventStore.AspNetCore;
using Clearly.EventSourcing;
using Clearly.EventSourcing.AspNetCore;
using Clearly.EventSubscription.EventStore.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Questionable.Queries.Denormalizers.QuestionDetails;
using Questionable.Queries.Models;
using Questionable.Queries.QuestionSearch;
using Questionable.Questions.Commands.CommandHandlers;
using Questionable.Questions.Commands.Commands;
using Questionable.Questions.Events.EventHandlers;
using Questionable.Questions.Events.Events;
using Questionable.Questions.Http.Mappings;
using Repository.Memory;
using Repository.Core;

namespace Questionable.Startup;

public class Startup
{
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        ConfigureMvc(services);
        ConfigureAggregates(services);
        ConfigureDomainEvents(services);
        ConfigureCommands(services);
        ConfigureMappings(services);

        //TODO: Move out
        services.AddEventStore(ApplyEventStoreSettings);

        services.AddScoped<QuestionDetailDenormalizer>();
        services.AddSingleton<ICommandFactory, ValidatedCommandFactory>();
        services.AddScoped<IQuestionSearcher, QuestionSearcher>();
        services.AddScoped<IRepository<Question>, MemoryRepository<Question>>();
        services.AddScoped<IQuery<Question>, MemoryRepository<Question>>();
        services.AddSingleton(typeof(MemoryDatabase<>));

        return services.BuildServiceProvider();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void ConfigureMvc(IServiceCollection services)
    {
        services
            .AddMvc()
            .AddSubscriptions();
    }

    private static void ApplyEventStoreSettings(EventStoreSettings settings)
    {
        // TODO: Pull from config
        settings.EndPoint = new IPEndPoint(IPAddress.Loopback, 1113);
        settings.EventTypes["QuestionAskedEvent"] = typeof(QuestionAskedEvent);
        settings.EventTypes["QuestionTaggedEvent"] = typeof(QuestionTaggedEvent);
        settings.EventTypes["QuestionAnsweredEvent"] = typeof(QuestionAnsweredEvent);
        settings.EventTypes["QuestionLikedEvent"] = typeof(QuestionLikedEvent);
        settings.EventTypes["AnswerAcceptedEvent"] = typeof(AnswerAcceptedEvent);
    }

    private static void ConfigureAggregates(IServiceCollection services)
    {
        services.AddAggregate<Questions.Aggregates.Question>()
            .AddEvent<QuestionAskedEvent>()
            .AddEvent<QuestionTaggedEvent>()
            .AddEvent<QuestionLikedEvent>()
            .AddEvent<QuestionAnsweredEvent>()
            .AddEvent<AnswerAcceptedEvent>();
    }

    private static void ConfigureDomainEvents(IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAskedEvent>, QuestionAskedHandler>();
        services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionTaggedEvent>, QuestionTaggedHandler>();
        services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionLikedEvent>, QuestionLikedHandler>();
        services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAnsweredEvent>, QuestionAnsweredHandler>();
        services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, AnswerAcceptedEvent>, AnswerAcceptedHandler>();
    }

    private static void ConfigureCommands(IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<AskQuestionCommand>, AskQuestionCommandHandler>();
        services.AddScoped<ICommandHandler<LikeQuestionCommand>, LikeQuestionCommandHandler>();
        services.AddScoped<ICommandHandler<AnswerQuestionCommand>, AnswerQuestionCommandHandler>();
        services.AddScoped<ICommandHandler<AcceptAnswerCommand>, AcceptAnswerCommandHandler>();
    }

    private static void ConfigureMappings(IServiceCollection services)
    {
        services.AddScoped<IMapper>(x => new Mapper(CreateMapperConfiguration()));
    }

    private static AutoMapper.IConfigurationProvider CreateMapperConfiguration()
    {
        return new MapperConfiguration(y => y.AddProfile<HttpCommandMappingProfile>());
    }
}
