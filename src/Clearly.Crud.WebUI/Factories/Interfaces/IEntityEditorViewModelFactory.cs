// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;
using Clearly.Crud.WebUi.ViewModels;

namespace Clearly.Crud.WebUi.Factories;

public interface IEntityEditorViewModelFactory<TEntity>
    where TEntity : IEntity
{
    EntityEditorViewModel Build(TEntity value);
}
