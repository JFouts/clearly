namespace DomainModeling.Attributes.UI;

public class FieldViewComponentAttribute : Attribute
{
    public string ViewComponentName { get; set; }

    public FieldViewComponentAttribute(string viewComponentName)
    {
        ViewComponentName = viewComponentName;
    }
}