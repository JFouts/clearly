﻿using Clearly.Crud.WebUi.Client.Shared.DisplayComponents;

namespace Clearly.Crud.WebUi.Client.Models
{
    public class ColumnDefinition
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string DisplayComponent { get; set; } = nameof(TextDisplayComponent);
    }
}
