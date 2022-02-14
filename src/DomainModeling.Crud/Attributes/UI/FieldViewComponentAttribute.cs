namespace DomainModeling.Attributes.UI;

public class FieldEditorAttribute : Attribute
{
    public string ViewComponentName { get; set; }

    public FieldEditorAttribute(string viewComponentName)
    {
        ViewComponentName = viewComponentName;
    }
}

public class FieldEditorPropertyAttribute : Attribute
{
    public string Name { get; set; } = string.Empty;
    public object Value { get; set; } = string.Empty;
    
    public FieldEditorPropertyAttribute(string name, object value)
    {
        Name = name;
        Value = value;
    }
}