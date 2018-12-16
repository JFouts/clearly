using System;
using System.Threading.Tasks;
using DomainModeling.Core.DomainObjectTypes;
using DomainModeling.Core.Handlers;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Xunit;

namespace DomainModeling.Test.Unit.ObjectSourcing.Scripting {
    public class BasicScript {
      [Fact]
      public async Task ItRunsTheScript() {
        var scriptText = System.IO.File.ReadAllText("sample.cs");

        var exec = await CSharpScript.RunAsync(
          scriptText,
          ScriptOptions.Default.WithReferences(
            typeof(Entity).Assembly,
            typeof(Guid).Assembly));

        var result =  exec.ReturnValue;
        Assert.IsAssignableFrom(typeof(CommandHandler), result);
      }
    }
}
