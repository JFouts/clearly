// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud;

public abstract class BaseDefinition
{
    protected List<IMetadata> Metadata { get; } = new List<IMetadata>();

    public TMetadata UsingMetadata<TMetadata>()
        where TMetadata : class, IMetadata, new()
    {
        var metadata = Metadata.OfType<TMetadata>().SingleOrDefault();

        if (metadata == null)
        {
            metadata = new TMetadata();
            Metadata.Add(metadata);
        }

        return metadata;
    }
}
