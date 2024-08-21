using Newtonsoft.Json;

namespace AspDotNetCoreProject.Context.Common
{
    public class AppSettings
    {
        public required Logging Logging { get; set; }
        public required string AllowedHosts { get; set; }
        public required DatabaseConfig DatabaseConfig { get; set; }
        public required JwtConfig JwtConfig { get; set; }
    }

    public class Logging
    {
        public required LogLevel LogLevel { get; set; }
    }

    public class LogLevel()
    {
        public required string Default { get; set; }
        [JsonProperty("Microsoft.AspNetCore")]
        public required string MicrosoftAspNetCore { get; set; }
    }

    public class DatabaseConfig()
    {
        public required string ConnectionString { get; set; }
    }

    public class JwtConfig()
    {
        public required string Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public required int ExpireMinutes { get; set; }
    }
}
