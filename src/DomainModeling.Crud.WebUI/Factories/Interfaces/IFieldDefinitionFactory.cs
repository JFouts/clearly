// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IFieldDefinitionFactory : IService
{
    EntityFieldDefinition CreateFieldDefinition(PropertyInfo property);
}
