
namespace Mc2.CrudTest.Shared.Configuration.Swagger
{
    public class SwaggerConfiguration
    {
        public bool Enable { get; set; }
        public bool EnableVersioning { get; set; }
        public bool EnableOAuthSupport { get; set; }
        public SwaggerDoc SwaggerDoc { get; set; }
        public SwaggerAuthorizationConfiguration Authorization { get; set; }
        public string SwaggerBasePath { get; set; }
        public string SwaggerHost { get; set; }
    }

    public class SwaggerAuthorizationConfiguration
    {
        public SwaggerApiScope[] Scopes { get; set; }
        public string ClientId { get; set; }
        public string ApiName { get; set; }
        public bool RequirePkce { get; set; }
    }

    public class SwaggerApiScope
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}