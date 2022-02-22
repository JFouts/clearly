// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Crud.Test.Unit;

public record FullySpecifiedEntity : FullySpecifiedEntityBase
{
    public object? PublicField;
    private object? PrivateField;
    internal object? InternalField;
    protected object? ProtectedField;
    protected internal object? ProtectedInternalField;
    private protected object? PrivateProtectedField;

    public object? PublicProperty { get; set; }
    public virtual object? PublicVirtualProperty { get; set; }
    public override object? PublicOverriddenBaseProperty { get; set; }
    public new object? PublicHiddenBaseProperty { get; set; }
    public object? PublicReadonlyProperty { get; }
    public object? PublicComputedProperty => null;

    private object? PrivateProperty { get; set; }
    private object? PrivateReadonlyProperty { get; }
    private object? PrivateComputedProperty => null;

    internal object? InternalProperty { get; set; }
    internal virtual object? InternalVirtualProperty { get; set; }
    internal override object? InternalOverriddenBaseProperty { get; set; }
    internal new object? InternalHiddenBaseProperty { get; set; }
    internal object? InternalReadonlyProperty { get; }
    internal object? InternalComputedProperty => null;

    protected internal object? ProtectedInternalProperty { get; set; }
    protected internal virtual object? ProtectedInternalVirtualProperty { get; set; }
    protected internal override object? ProtectedInternalOverriddenBaseProperty { get; set; }
    protected internal new object? ProtectedInternalHiddenBaseProperty { get; set; }
    protected internal object? ProtectedInternalReadonlyProperty { get; }
    protected internal object? ProtectedInternalComputedProperty => null;
    
    private protected object? PrivateProtectedProperty { get; set; }
    private protected virtual object? PrivateProtectedVirtualProperty { get; set; }
    private protected override object? PrivateProtectedOverriddenBaseProperty { get; set; }
    private protected new object? PrivateProtectedHiddenBaseProperty { get; set; }
    private protected object? PrivateProtectedReadonlyProperty { get; }
    private protected object? PrivateProtectedComputedProperty => null;
}