// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.JsonLd;

public class EntityApiFeature : IEntityFeature
{
    public string Id { get; set; }
    public string Method { get; set; } = "GET";
    public string UrlFormat { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
