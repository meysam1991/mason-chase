﻿namespace Mc2.CrudTest.ModelFramework.Translations
{
    public interface ITranslator
    {
        string this[string name]
        {
            get;
            set;
        }
        string this[string name, params string[] arguments]
        {
            get;
            set;
        }
        string GetString(string name);
        string GetString(string name, params string[] arguments);

    }
}
