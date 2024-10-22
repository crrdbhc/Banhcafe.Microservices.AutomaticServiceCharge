namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;

public static class DatabaseSettingsInstances
{
    public static string SQL = "DatabaseSettings";
}

public sealed class DatabaseSettings
{
    public string SchemeName { get; set; }
    public string SchemeNameServ { get; set; }
    public string SchemeNameCat { get; set; }
    public string DatabaseName { get; set; }
    public string DatabaseApiUrl { get; set; }
}
