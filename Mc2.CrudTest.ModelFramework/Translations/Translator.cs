using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Mc2.CrudTest.ModelFramework.Translations
{
    public class Translator : ITranslator
    {
        public string this[string name] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string this[string name, params string[] arguments] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        private readonly ResourceManager _rm;

        public Translator()
        {
            _rm = new ResourceManager("SirvanTspTaxpayerResource.Resources.ExceptionMessages", Assembly.GetExecutingAssembly());

        }
        public string GetString(string name)
        {
            return _rm.GetString(name, CultureInfo.CurrentCulture);
        }

        public string GetString(string name, params string[] arguments)
        {
            var value = _rm.GetString(name, CultureInfo.CurrentCulture);
            return string.Format(value, arguments);
        }
    }
}
