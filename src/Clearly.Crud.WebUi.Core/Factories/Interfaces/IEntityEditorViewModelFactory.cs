// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

/// <summary>
/// Factory to create an <see cref="EntityEditorViewModel"/> from a <see cref="IEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The entity type of your model.</typeparam>
public interface IEntityEditorViewModelFactory<TEntity>
    where TEntity : IEntity
{
    /// <summary>
    /// Creates an <see cref="EntityEditorViewModel"/> from a <see cref="IEntity"/>.
    /// </summary>
    /// <param name="value">The entity model.</param>
    /// <returns>The view model for the entity.</returns>
    EntityEditorViewModel Build(TEntity value);
}
