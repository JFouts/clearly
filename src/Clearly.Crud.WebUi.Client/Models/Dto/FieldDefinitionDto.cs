// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;

namespace Clearly.Crud.WebUi.Client.Models.Dto
{
    public record FieldDefinitionDto
    {
        public string NameKey { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public Dictionary<string, JToken> Features { get; set; } = new Dictionary<string, JToken>();
    }
}
