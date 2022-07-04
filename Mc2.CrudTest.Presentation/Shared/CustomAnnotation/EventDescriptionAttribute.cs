using System;

namespace Mc2.CrudTest.Shared.CustomAnnotation
{
    public class EventDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public EventDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}