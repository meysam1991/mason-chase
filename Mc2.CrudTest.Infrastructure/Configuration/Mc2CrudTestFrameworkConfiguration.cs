using Mc2.CrudTest.Infrastructure.Configuration.Swagger;
using Mc2.CrudTest.Shared.Configuration.PoolingPublisher;

namespace Mc2.CrudTest.Infrastructure.Configuration
{
    public class Mc2Mc2CrudTestFrameworkConfiguration
    {
        public string ServiceId { get; set; }
        public string JsonSerializerTypeName { get; set; }
        public string ExcelSerializerTypeName { get; set; }
        public string UserInfoServiceTypeName { get; set; }
        public bool TrackActionPerformanceEnabled { get; set; }
        public bool UseFakeUserService { get; set; }
        public string[] AssemblyNamesForLoad { get; set; }

        public SwaggerConfiguration Swagger { get; set; }
        public string SiteLocale { get; set; }
        public PoolingPublisherConfiguration PoolingPublisher { get; set; }


    }
}