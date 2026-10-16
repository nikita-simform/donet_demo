namespace LibraryApi.Configuration;

// DAY 3: CONFIGURATION
// Strongly-typed binding target for the "ApiSettings" section of appsettings.json.
// Bound in Program.cs via builder.Services.Configure<ApiSettings>(...).
public class ApiSettings
{
    public string ApplicationName { get; set; } = "";
    public int MaxPageSize { get; set; }
}
