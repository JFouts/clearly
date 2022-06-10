﻿// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Text.Json;

namespace Clearly.Crud.WebUi.Client.Models.Dto
{
    public record EntityDefinitionDto
    {
        public string NameKey { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public IEnumerable<FieldDefinitionDto> Fields { get; set; } = Array.Empty<FieldDefinitionDto>();
        /// public Dictionary<string, JsonElement> Features { get; set; } = new Dictionary<string, JsonElement>();
    }
}