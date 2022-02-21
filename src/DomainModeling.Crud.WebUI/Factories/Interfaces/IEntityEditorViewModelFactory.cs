// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using DomainModeling.Core;
using DomainModeling.Crud.WebUi.ViewModels;

namespace DomainModeling.Crud.WebUi.Factories;

public interface IEntityEditorViewModelFactory<TEntity>
    where TEntity : IEntity
{
    EntityEditorViewModel Build(TEntity value);
}
