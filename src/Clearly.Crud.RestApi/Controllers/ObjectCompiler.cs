// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using System.Reflection.Emit;
using Clearly.Core;
using Clearly.Crud.EntityGraph;
namespace Clearly.Crud.RestApi;

public abstract class ObjectCompiler
{
    private readonly ModuleBuilder module;

    public ObjectCompiler(string assemblyName)
    {
        this.module = CreateNewAssemblyModule(assemblyName);
    }

    public Type Compile(ObjectTypeDefinitionNode type)
    {
        // TODO: This should maintain a collection of compiled DTOs so that we can be idempotent
        return CreateNewType(module, type);
    }

    private static ModuleBuilder CreateNewAssemblyModule(string assemblyName)
    {
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            new AssemblyName(assemblyName), 
            AssemblyBuilderAccess.Run);

        return assemblyBuilder.DefineDynamicModule(assemblyName);
    }

    private Type CreateNewType(ModuleBuilder module, ObjectTypeDefinitionNode sourceType)
    {
        var typeBuilder = module.DefineType(GetTypeName(sourceType),
                TypeAttributes.Public |
                TypeAttributes.Class |
                TypeAttributes.AutoClass |
                TypeAttributes.AnsiClass |
                TypeAttributes.BeforeFieldInit |
                TypeAttributes.AutoLayout,
                null);

        typeBuilder.AddInterfaceImplementation(typeof(IEntity));

        var constructor = typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

        foreach (var property in sourceType.Properties)
        {
            if (property.Property.PropertyType.IsAssignableTo(typeof(IEntity)))
            {
                CreateProperty(typeBuilder, property.Property.Name, typeof(Guid));
            }
            else if (property.Property.PropertyType.IsAssignableTo(typeof(IEnumerable<IEntity>)))
            {
                CreateProperty(typeBuilder, property.Property.Name, typeof(IEnumerable<Guid>));
            }
            else
            {
                CreateProperty(typeBuilder, property.Property.Name, property.Property.PropertyType);
            }
        }

        return typeBuilder.CreateType() ?? throw new NullReferenceException();
    }

    protected abstract string GetTypeName(ObjectTypeDefinitionNode typeDefinition);

    protected abstract void HandleProperty(PropertyDefinitionNode property, TypeBuilder typeBuilder);

    protected static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
    {
        var fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

        var propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
        var getPropertyMethodBuilder = 
            tb.DefineMethod("get_" + propertyName,
                MethodAttributes.Public | 
                MethodAttributes.SpecialName | 
                MethodAttributes.HideBySig | 
                MethodAttributes.Virtual, 
                returnType: propertyType, 
                parameterTypes: Type.EmptyTypes);
        var getIl = getPropertyMethodBuilder.GetILGenerator();

        getIl.Emit(OpCodes.Ldarg_0);
        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
        getIl.Emit(OpCodes.Ret);

        var setPropertyMethodBuilder =
            tb.DefineMethod("set_" + propertyName,
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.HideBySig | 
                MethodAttributes.Virtual,
                returnType: null, 
                parameterTypes: new[] { propertyType });

        var setIl = setPropertyMethodBuilder.GetILGenerator();
        var modifyProperty = setIl.DefineLabel();
        var exitSet = setIl.DefineLabel();

        setIl.MarkLabel(modifyProperty);
        setIl.Emit(OpCodes.Ldarg_0);
        setIl.Emit(OpCodes.Ldarg_1);
        setIl.Emit(OpCodes.Stfld, fieldBuilder);

        setIl.Emit(OpCodes.Nop);
        setIl.MarkLabel(exitSet);
        setIl.Emit(OpCodes.Ret);

        propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
        propertyBuilder.SetSetMethod(setPropertyMethodBuilder);

        if (propertyName == "Id")
        {
            tb.DefineMethodOverride(getPropertyMethodBuilder, typeof(IEntity).GetProperty("Id")!.GetGetMethod()!);
            tb.DefineMethodOverride(setPropertyMethodBuilder, typeof(IEntity).GetProperty("Id")!.GetSetMethod()!);
        }
    }
}
