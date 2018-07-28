using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using DomainModeling.EventRepository.EventStore;
using DomainModeling.EventSourcing.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DomainModeling.Test.Unit.Framework
{
    public class ApplicationBuilderTests
    {
        private readonly ApplicationBuilder _builder;
        
        public ApplicationBuilderTests()
        {
            _builder = new ApplicationBuilder();
        }

        [Fact]
        public void BuildExists()
        {
            _builder.Build();
        }

        [Fact]
        public void BuildReturnsAnAspNetCoreApplication()
        {
            var application = _builder.Build();
            Assert.IsAssignableFrom<IWebHost>(application);
        }

        [Fact]
        public void ApplicationBuilderTakesInApplicationDefinition()
        {
            var def = new ApplicationDefinition();
            _builder.WithApplicationDefinition(def);
            _builder.Build();
        }

        [Fact]

        public void GenerateSampleViewModel()
        {
            const string code = 
@"namespace Models {
    public class Item {
        public string ItemName { get; set; }
    }
}
";

            var tree = SyntaxFactory.ParseSyntaxTree(code);
            var fileName = Path.GetRandomFileName();
            
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            var compilation = CSharpCompilation
                .Create(fileName)
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .WithReferences(references)
                .AddSyntaxTrees(tree);
                     
            using (var stream = new MemoryStream())
            {
                var result = compilation.Emit(stream);

                if (!result.Success)
                {
                    var failures = result.Diagnostics.Where(diagnostic => 
                        diagnostic.IsWarningAsError || 
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (var diagnostic in failures)
                    {
                        var issue = $"ID: {diagnostic.Id}, Message: {diagnostic.GetMessage()}, Location: {diagnostic.Location.GetLineSpan()}, Severity: {diagnostic.Severity}";
                        Assert.Equal("", issue);
                    }

                    return;
                }
                else
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(stream.ToArray());
                }
            }
        }
    }
}

public class ApplicationDefinition {
    public int Port { get; set; }
    public IEnumerable<EventTypeDefinition> EventTypes { get; internal set; }

    public class EventTypeDefinition {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}



public class ApplicationBuilder
{
    private ApplicationDefinition _appDefinition;
    public void WithApplicationDefinition(ApplicationDefinition appDefintion)
    {
        _appDefinition = appDefintion;
    }

    public IWebHost Build()
    {
        var builder = WebHost.CreateDefaultBuilder();
        builder.ConfigureServices(x => x.AddSingleton(_appDefinition ?? new ApplicationDefinition()));
        builder.UseStartup<ManagedStartup>();
        return builder.Build(); 
    }

    private class ManagedStartup : IStartup
    {
        public readonly ApplicationDefinition _appDefinition;

        public ManagedStartup(ApplicationDefinition appDefinition)
        {
            _appDefinition = appDefinition;
        }

        public void Configure(IApplicationBuilder app)
        {        
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
            ConfigureAggregates(services);
            ConfigureDomainEvents(services);
            ConfigureCommands(services);
            ConfigureMappings(services);

            //TODO: Move out
            //services.AddEventStore(ApplyEventStoreSettings);

            //services.AddScoped<QuestionDetailDenormalizer>();
            //services.AddSingleton<ICommandFactory, ValidatedCommandFactory>();
            //services.AddScoped<IQuestionSearcher, QuestionSearcher>();
            //services.AddScoped<IRepository<Question>, MemoryRepository<Question>>();
            //services.AddScoped<IQuery<Question>, MemoryRepository<Question>>();
           
            //services.AddSingleton(typeof(MemoryDatabase<>));

            return services.BuildServiceProvider();
        }

        private static void ConfigureMvc(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddDataAnnotations()
                // .AddSubscriptions()                
                // .AddApplicationPart(Assembly.Load(new AssemblyName("Questionable.Questions.Http")))
                // .AddApplicationPart(Assembly.Load(new AssemblyName("Questionable.Queries.Http")))
                // .AddApplicationPart(Assembly.Load(new AssemblyName("Questionable.Queries.Denormalizer.QuestionSearch")))
                ;
        }

        private void ApplyEventStoreSettings(EventStoreSettings settings) {
            settings.EndPoint = new IPEndPoint(IPAddress.Loopback, _appDefinition.Port);

            foreach(var eventType in _appDefinition.EventTypes) {
                var type = Type.GetType(eventType.Type);
                settings.EventTypes[eventType.Name] = type;
            }
        }

        private static void ConfigureAggregates(IServiceCollection services)
        {
            /*
            foreach(var aggregate in _appDefinition.Aggregates) {
                services.AddAggregate();
            }

            services.AddAggregate<Questions.Aggregates.Question>()
                .AddEvent<QuestionAskedEvent>()
                .AddEvent<QuestionTaggedEvent>()
                .AddEvent<QuestionLikedEvent>()
                .AddEvent<QuestionAnsweredEvent>()
                .AddEvent<AnswerAcceptedEvent>();*/
        }

        private static void ConfigureDomainEvents(IServiceCollection services)
        {
            /*services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAskedEvent>, QuestionAskedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionTaggedEvent>, QuestionTaggedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionLikedEvent>, QuestionLikedHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, QuestionAnsweredEvent>, QuestionAnsweredHandler>();
            services.AddScoped<IDomainEventHandler<Questions.Aggregates.Question, AnswerAcceptedEvent>, AnswerAcceptedHandler>();*/
        }

        private static void ConfigureCommands(IServiceCollection services)
        {
            /*services.AddScoped<ICommandHandler<AskQuestionCommand>, AskQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<LikeQuestionCommand>, LikeQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<AnswerQuestionCommand>, AnswerQuestionCommandHandler>();
            services.AddScoped<ICommandHandler<AcceptAnswerCommand>, AcceptAnswerCommandHandler>();*/
        }

        private static void ConfigureMappings(IServiceCollection services)
        {
            //services.AddScoped<IMapper>(x => new Mapper(CreateMapperConfiguration()));
        }

        /*private static IConfigurationProvider CreateMapperConfiguration()
        {
            return new MapperConfiguration(y => y.AddProfile<HttpCommandMappingProfile>());
        }*/
    }
}