using System;

namespace Mc2.CrudTest.Shared.CustomAnnotation
{
    public class ActionDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public ActionDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
