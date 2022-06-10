// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.WebUi.Client.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Clearly.Crud.RestApi;

[Route("api/entity")]
public class EntityDefinitionController : ControllerBase
{
    private readonly ITypeProvider entityTypeProvider;
    private readonly IEntityDefinitionFactory entityDefinitionFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDefinitionController"/> class.
    /// </summary>
    /// <param name="entityTypeProvider"></param>
    /// <param name="entityDefinitionFactory"></param>
    public EntityDefinitionController(ITypeProvider entityTypeProvider, IEntityDefinitionFactory entityDefinitionFactory)
    {
        this.entityTypeProvider = entityTypeProvider;
        this.entityDefinitionFactory = entityDefinitionFactory;
    }

    [HttpGet]
    public IActionResult GetAllEntities()
    {
        var supportedTypes = entityTypeProvider.GetTypes()
            .Select(x => entityDefinitionFactory.CreateForType(x))
            .Select(x => new { x.NameKey, x.DisplayName })
            .ToList();

        return Ok(supportedTypes);
    }

    [HttpGet("{nameKey}")]
    public IActionResult GetByNameKey([FromRoute] string nameKey)
    {
        var supportedType = entityTypeProvider.GetTypes()
            .Select(x => entityDefinitionFactory.CreateForType(x))
            .FirstOrDefault(x => string.Equals(x.NameKey, nameKey, StringComparison.InvariantCultureIgnoreCase));

        if (supportedType == null)
        {
            return NotFound();
        }

        return Ok(ToDto(supportedType));
    }

    private EntityDefinitionDto ToDto(EntityDefinition entityDefinition)
    {
        return new EntityDefinitionDto
        {
            DisplayName = entityDefinition.DisplayName,
            NameKey = entityDefinition.NameKey,
            Fields = entityDefinition.Fields.Select(ToDto),
            Features = entityDefinition.RegisteredFeatures.ToDictionary(SelectKey, AsObject),
        };
    }

    private FieldDefinitionDto ToDto(FieldDefinition fieldDefinition)
    {
        return new FieldDefinitionDto
        {
            DisplayName = fieldDefinition.DisplayName,
            NameKey = fieldDefinition.Property.Name,
            Features = fieldDefinition.RegisteredFeatures.ToDictionary(SelectKey, AsObject),
        };
    }

    private string SelectKey(IObjectFeature feature)
    {
        return feature.GetType().Name;
    }

    private string SelectKey(IFieldFeature feature)
    {
        return feature.GetType().Name;
    }

    private object AsObject<T>(T o)
    {
        return o!;
    }
}
