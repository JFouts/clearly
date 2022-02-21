// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc.Razor;

namespace DomainModeling.Crud.WebUi.Infrastructure;

public class GenericControllerViewLocationExpander : IViewLocationExpander
{
    private const string GENERIC_CONTROLLER_TEXT = "Controller`1";
    private const string CONTROLLER_NAME_FORMAT_FIELD = "{1}";

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        if (context.ControllerName != null)
        {
            if (context.ControllerName?.EndsWith(GENERIC_CONTROLLER_TEXT) ?? false)
            {
                var updatedControllerName = context.ControllerName.Replace(GENERIC_CONTROLLER_TEXT, string.Empty);
                var updatedViewLocations = viewLocations.Select(x => x.Replace(CONTROLLER_NAME_FORMAT_FIELD, updatedControllerName));

                return updatedViewLocations;
            }
        }

        return viewLocations;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}
