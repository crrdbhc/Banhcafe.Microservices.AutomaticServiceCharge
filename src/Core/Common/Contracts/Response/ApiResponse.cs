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

    public void AddPagination(BaseQueryResponseDto dto)
    {
        Metadata ??= new Dictionary<string, object>();
        Metadata.Add("currentPage", dto.CurrentPage);
        Metadata.Add("size", dto.Size);
        Metadata.Add("lastPage", dto.TotalRows);
        Metadata.Add("totalRows", dto.TotalCount);
        Metadata.Add("rowId", dto.RowId);
        Metadata.Add("page", dto.CurrentPage);
    }
}

public abstract class BaseQueryResponseDto
{
    public string? Message { internal get; set; }
    public bool Success { internal get; set; } = true;
    public int? CurrentPage { internal get; set; }
    public int? Page { internal get; set; }
    public int? RowId { internal get; set; }
    public int? LastPage { internal get; set; }
    public int? Size { internal get; set; }
    public int? TotalCount { internal get; set; }
    public int? TotalRows { internal get; set; }
}

public class BaseQuery
{
    public int? Page { get; set; }
    public int? Size { get; set; }
}

public interface IBaseQueryDto
{
    public int? TerminalId { get; set; }
    public int? Page { get; set; }
    public int? Size { get; set; }
}

public interface IBaseQuerySearchDto
{
    public int? Page { get; set; }
    public int? Size { get; set; }
}

public interface IBaseQueryResponseDto
{
    public string Message { get; set; }
    public bool Success { get; set; }
}

public interface IBaseCreateDto
{
    public int CreatorId { get; set; }
}

public interface IBaseUpdateDto
{
    public int UpdatedId { get; set; }
}

public interface IBaseDeleteDto
{
    public int DeletedId { get; set; }
}