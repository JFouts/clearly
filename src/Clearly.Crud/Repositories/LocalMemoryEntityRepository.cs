// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Concurrent;
using System.Linq.Expressions;
using Clearly.Core;
using Clearly.Crud;
using Clearly.Crud.Search;
using static System.Linq.Expressions.Expression;

namespace Clearly.EntityRepository;

// TODO: This doesn't belong in here

/// <summary>
/// An entity repository that uses local memory as it's data store.
/// </summary>
/// <remarks>
/// This data store will clear when the application shuts down. This is intended to be used for testing purposes only.
/// </remarks>
/// <typeparam name="T">The data type of the Entity managed by this repository.</typeparam>
public class LocalMemoryEntityRepository<T> : IEntityRepository<T>
    where T : IEntity
{
    private static readonly ConcurrentDictionary<Guid, T> Table = new ();

    /// <inheritdoc/>
    public Task Delete(Guid id)
    {
        Table.TryRemove(id, out _);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<T> GetById(Guid id)
    {
        if (Table.TryGetValue(id, out var result))
        {
            return Task.FromResult(result);
        }

        throw new KeyNotFoundException($"Entity with id {id} was not found in table {typeof(T).Name}.");
    }

    /// <inheritdoc/>
    public Task Insert(T obj)
    {
        Table.TryAdd(obj.Id, obj);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<CrudSearchResult<T>> Search(CrudSearchOptions options)
    {
        var result = new CrudSearchResult<T>
        {
            Skip = options.Skip,
            Take = options.Take,
        };

        var query = Table.Values.AsQueryable();

        if (!string.IsNullOrWhiteSpace(options.SearchQuery))
        {
            Expression? condition = null;
            var parameter = Parameter(typeof(T), "x");

            // TODO: Configurable in Entity Definition
            foreach (var property in typeof(T).GetProperties())
            {
                if (property.PropertyType.IsAssignableTo(typeof(string)))
                {
                    // TODO: I'd hate to make people conform to this paradigm when implementing their own repos
                    // Maybe there is a way for the expressions to be build when the entity definition is built
                    // Then they can just use a where clause in their repo.
                    var match = Call(
                        Property(parameter, property),
                        typeof(string).GetMethod("Contains", new [] 
                        { 
                            typeof(string),
                            typeof(StringComparison),
                        }) !, 
                        new Expression[]
                        {
                            Constant(options.SearchQuery, typeof(string)),
                            Constant(StringComparison.InvariantCultureIgnoreCase, typeof(StringComparison)),
                        });

                    condition = condition == null ? match : Or(condition, match);
                }
            }

            if (condition != null)
            {
                query = query.Where((Expression<Func<T, bool>>)Lambda(condition, "WhereClause", new [] { parameter }));
            }
        }

        result.Count = query.Count();

        if (options.Skip > 0)
        {
            query = query.Skip(options.Skip);
        }

        if (options.Take > 0)
        {
            query = query.Take(options.Take);
        }

        result.Results = query;
        
        // TODO: result.Results = query.ToAsyncEnumerable();
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task Update(Guid id, T obj)
    {
        Table.AddOrUpdate(id, obj, (x, y) => obj);

        return Task.CompletedTask;
    }
}
