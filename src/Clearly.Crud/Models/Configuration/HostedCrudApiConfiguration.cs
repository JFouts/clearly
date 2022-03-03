// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.Services;

public record HostedCrudApiConfiguration
{
    public string BaseUrl { get; set; } = string.Empty;
}
