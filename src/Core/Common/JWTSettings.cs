namespace Banhcafe.Microservices.AutomaticServiceCharge.Core.Common;

public static class JWTSettingsSections
{
    public static string External = "ExternalJWTSettings";
    public static string Internal = "InternalJWTSettings";
}

public sealed class JWTSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
}
