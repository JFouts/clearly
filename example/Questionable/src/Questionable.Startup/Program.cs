using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Questionable.Startup;

WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build()
    .Run();
