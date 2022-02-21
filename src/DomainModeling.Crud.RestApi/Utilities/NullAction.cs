// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.RestApi.Utilities;

internal static class NullAction<T>
{
    public static Action<T> NoOp()
    {
        return _ =>
        {
        };
    }
}
