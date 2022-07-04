using System;

namespace Mc2.CrudTest.Shared.CustomAnnotation
{
    public class ControllerDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public ControllerDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
