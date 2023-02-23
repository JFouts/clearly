// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Crud.WebUi;

namespace Clearly.Crud.Test.Unit;

public static class ModulePresets
{
    public static IEnumerable<IDefinitionNodeModule> DefaultModules { get; } = new IDefinitionNodeModule[]
    {
        new AttributeBasedEntityModule(),
        new CoreEntityModule(),
        new CrudAdminModule(),
        new AttributeBasedEntityFieldModule(),
        new CoreEntityFieldModule(),
    };

    public static IEnumerable<IDefinitionNodeModule> NoModules { get; } = new IDefinitionNodeModule[0];
}
