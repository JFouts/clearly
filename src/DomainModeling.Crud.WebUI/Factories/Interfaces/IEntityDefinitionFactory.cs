// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.WebUi;

public interface IEntityDefinitionFactory
{
    EntityDefinition CreateForType(Type entity);
}
