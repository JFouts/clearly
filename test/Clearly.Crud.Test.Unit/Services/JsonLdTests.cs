// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text;
using System.Text.Json;
using Clearly.Core;
using Clearly.Crud.JsonLd;
using Clearly.Crud.EntityGraph;
using Clearly.Crud.WebUi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Clearly.Crud.Test.Unit;

public class JsonLdTests
{
    private readonly IServiceCollection services;
    private readonly DefaultHttpContext httpContext;
    private readonly MockOutputFormatterCanWriteContext canWriteContext;
    private readonly Guid entityId;
    private readonly StringBuilder outputBuffer;
    
    private JsonLdObjectConverterFactory factory = default!;
    private SystemTextJsonLdOutputFormatter formatter = default!;
    private MockOutputFormatterWriteContext writeContext = default!;

    public JsonLdTests()
    {
        outputBuffer = new StringBuilder();
        entityId = Guid.Parse("1dc64d2b-d3f4-472b-9b41-5e95d8d888ed");

        services = new ServiceCollection()
            .AddSingleton<IDefinitionNodeModule, AttributeBasedEntityFieldModule>()
            .AddSingleton<IDefinitionNodeModule, AttributeBasedEntityModule>()
            .AddSingleton<IDefinitionNodeModule, CoreEntityFieldModule>()
            .AddSingleton<IDefinitionNodeModule, CoreEntityModule>()
            .AddSingleton<IDefinitionNodeModule, CrudAdminModule>()
            .AddSingleton<IEntityDefinitionGraphFactory, EntityDefinitionGraphFactory>();

        httpContext = new DefaultHttpContext();
        httpContext.Response.Body = new MemoryStream();
        canWriteContext = new MockOutputFormatterCanWriteContext(httpContext);

        UsingEntity(new BlankEntity(entityId));
    }

    [Fact]
    public void Formatter_DoesNotApplyToApplicationJson()
    {
        canWriteContext.ObjectType = typeof(BlankEntity);
        canWriteContext.ContentType = "application/json";

        var result = formatter.CanWriteResult(canWriteContext);

        Assert.False(result);
    }

    [Fact]
    public void Formatter_DoesApplyToApplicationLdJson()
    {
        canWriteContext.ObjectType = typeof(BlankEntity);
        canWriteContext.ContentType = "application/ld+json";

        var result = formatter.CanWriteResult(canWriteContext);

        Assert.True(result);
    }

    [Fact]
    public void Formatter_DoesNotApplyToNonEntities()
    {
        canWriteContext.ObjectType = typeof(object);
        canWriteContext.ContentType = "application/ld+json";

        var result = formatter.CanWriteResult(canWriteContext);

        Assert.False(result);
    }

    [Fact]
    public async Task Formatter_OutputsAsJsonLd()
    {
        await formatter.WriteResponseBodyAsync(writeContext, Encoding.UTF8);

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        // TODO: Find a way to better express this json to be more readable
        var expected = "{\"@context\":{\"@version\":1.1,\"@vocab\":\"/schema/blankentity#\"},\"@type\":\"/schema/blankentity\",\"@id\":\"/api/blankentity/1dc64d2b-d3f4-472b-9b41-5e95d8d888ed\",\"id\":\"1dc64d2b-d3f4-472b-9b41-5e95d8d888ed\"}";
        Assert.Equal(expected, response);
    }

    [TypeSchema("Article", DefaultVocab = "https://schema.org")]
    private class Article : IEntity
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
    }

    [Fact]
    public async Task Formatter_ReadsTypeOverride()
    {
        UsingEntity(new Article { Id = entityId, Author = "Mr. Example Pants" });

        await formatter.WriteResponseBodyAsync(writeContext, Encoding.UTF8);

        httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        // Using Newtonsoft here rather than System.Text.Json because Newtonsoft is faster
        // a querying properties in the JSON
        var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(response);

        Assert.NotNull(json);
        var context = json!["@context"];
        Assert.NotNull(context);
        Assert.Equal("https://schema.org", (string?)context!["@vocab"]);
        Assert.Equal("Article", (string?)json["@type"]);
        Assert.Equal("Mr. Example Pants", (string?)json["author"]);
    }

    private void UsingEntity<TEntity>(TEntity entity)
        where TEntity : IEntity
    {
        services.AddSingleton<JsonLdObjectConverter<TEntity>>();
        writeContext = new MockOutputFormatterWriteContext(httpContext, WriterFactory, entity.GetType(), entity);

        factory = new JsonLdObjectConverterFactory(services.BuildServiceProvider());
        formatter = new SystemTextJsonLdOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web), factory);
    }

    private TextWriter WriterFactory(Stream stream, Encoding encoding)
    {
        return new StringWriter(outputBuffer);
    }

    private class MockOutputFormatterWriteContext : OutputFormatterWriteContext
    {
        public MockOutputFormatterWriteContext(HttpContext httpContext, Func<Stream, Encoding, TextWriter> writerFactory, Type? objectType, object? @object) 
            : base(httpContext, writerFactory, objectType, @object)
        {
        }
    }

    private class MockOutputFormatterCanWriteContext : OutputFormatterCanWriteContext
    {
        public new Type? ObjectType
        { 
            set
            {
                base.ObjectType = value;
            }
        }

        public MockOutputFormatterCanWriteContext(HttpContext httpContext)
            : base(httpContext)
        {
        }
    }
}
