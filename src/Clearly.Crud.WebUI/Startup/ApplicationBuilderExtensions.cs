// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;

namespace Clearly.Crud.WebUi;

public static class ApplicationBuilderExtensions
{
    public static WebApplication UseClearlyAdmin(this WebApplication app)
    {
        app.UseBlazorFrameworkFiles("/clearlycrudwebui");
        
        // TODO: Don't hard code the /admin route use Options pattern to allow user to change
        app.MapControllerRoute("clearlycrudwebui-fallback", "admin/{**catchall}", new { controller = "EntityCrudAdminRoot", action = "Index" });

        return app;
    }
}
