namespace Banhcafe.Microservices.ServiceChargingSystem.Core.Common.Contracts.Response;

public sealed class ApiResponse<TResponse>
{
    public IList<string> Errors { get; internal set; } = [];
    public IDictionary<string, string[]> ValidationErrors { get; set; } =
        new Dictionary<string, string[]>();
    public TResponse Data { get; set; } = default!;
    public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

    public void AddErrors(params string[] errors)
    {
        Errors ??= new List<string>();

        foreach (var error in errors)
        {
            Errors.Add(error);
        }
    }

    public void AddMetadata(string key, object value)
    {
        Metadata ??= new Dictionary<string, object>();

        if (value is not null)
            Metadata.Add(key, value);
    }
}
