// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core;

namespace Clearly.Crud.Test.Unit;

#pragma warning disable SA1401
#pragma warning disable SA1306
#pragma warning disable CS0649
#pragma warning disable CS0169
public record FullySpecifiedEntityBase : IEntity
{
    public Guid Id { get; set; }

    public object? PublicBaseField;
    private object? PrivateBaseField;
    internal object? InternalBaseField;
    protected object? ProtectedBaseField;
    protected internal object? ProtectedInternalBaseField;
    private protected object? PrivateProtectedBaseField;

    private object? PrivateBaseProperty { get; set; }
    private object? PrivateReadonlyBaseProperty { get; }
    private object? PrivateComputedBaseProperty => null;

    public object? PublicBaseProperty { get; set; }
    public virtual object? PublicVirtualBaseProperty { get; set; }
    public virtual object? PublicOverriddenBaseProperty { get; set; }
    public object? PublicHiddenBaseProperty { get; set; }
    public object? PublicReadonlyBaseProperty { get; }
    public object? PublicComputedBaseProperty => null;

    internal object? InternalBaseProperty { get; set; }
    internal virtual object? InternalVirtualBaseProperty { get; set; }
    internal virtual object? InternalOverriddenBaseProperty { get; set; }
    internal object? InternalHiddenBaseProperty { get; set; }
    internal object? InternalReadonlyBaseProperty { get; }
    internal object? InternalComputedBaseProperty => null;

    protected internal object? ProtectedInternalBaseProperty { get; set; }
    protected internal virtual object? ProtectedInternalVirtualBaseProperty { get; set; }
    protected internal virtual object? ProtectedInternalOverriddenBaseProperty { get; set; }
    protected internal object? ProtectedInternalHiddenBaseProperty { get; set; }
    protected internal object? ProtectedInternalReadonlyBaseProperty { get; }
    protected internal object? ProtectedInternalComputedBaseProperty => null;
    
    private protected object? PrivateProtectedBaseProperty { get; set; }
    private protected virtual object? PrivateProtectedVirtualBaseProperty { get; set; }
    private protected virtual object? PrivateProtectedOverriddenBaseProperty { get; set; }
    private protected object? PrivateProtectedHiddenBaseProperty { get; set; }
    private protected object? PrivateProtectedReadonlyBaseProperty { get; }
    private protected object? PrivateProtectedComputedBaseProperty => null;
}
#pragma warning restore SA1401
#pragma warning restore SA1306
#pragma warning restore CS0649
#pragma warning restore CS0169
