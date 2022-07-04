using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.Shared.CustomAnnotation
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extensions = GetAllowExtensionsFileValue(validationContext);
            if (file == null) return ValidationResult.Success;
            var extension = Path.GetExtension(file.FileName);
            return !extensions.Contains(extension.ToLower(CultureInfo.CurrentCulture)) ? new ValidationResult(string.Format(ErrorMessageString, extensions)) : ValidationResult.Success;
        }

        private static string[] GetAllowExtensionsFileValue(ValidationContext validationContext)
        {
            var configuration = (IConfiguration)validationContext.GetService(typeof(IConfiguration));
            var extensions = configuration.GetValue<string>("FileConfiguration:AllowExtensions");
            var listOfExtension = extensions.Split(',').ToArray();
            return listOfExtension;
        }
    }
}
