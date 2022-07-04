using Mc2.CrudTest.Shared.StringUtils;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
 
namespace SirvanTspSwitch.Framework.Database.SqlDatabase.EntityFramework.Extensions
{
    public static class DbContextExtensions
    {
        public static void ValidateStrings(this ChangeTracker tracker)
        {
            const char arabicYeChar = (char)1610;
            const char persianYeChar = (char)1740;

            const char arabicKeChar = (char)1603;
            const char persianKeChar = (char)1705;
            var entries = tracker.Entries();
            foreach (var entry in entries)
            {
                var stringProperties =
                    entry.Entity.GetType().GetProperties().Where(x => x.PropertyType == typeof(string)).ToList();

                foreach (var stringProperty in stringProperties)
                {
                    if (stringProperty.GetValue(entry.Entity) == null) continue;
                    var currentValue = stringProperty.GetValue(entry.Entity).ToString();
                    stringProperty.SetValue(entry.Entity,
                        currentValue.Replace(arabicYeChar, persianYeChar).Replace(arabicKeChar, persianKeChar).ConvertPersianNumbersToEnglishNumbers());
                }
            }
        }
    }
}
