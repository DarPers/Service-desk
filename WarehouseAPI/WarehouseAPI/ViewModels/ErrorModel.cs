using System.Net;
using System.Text.Json;

namespace WarehouseAPI.ViewModels;

public class ErrorModel
{
    public Guid ErrorId { get; set; } = Guid.NewGuid();

    public string Message { get; set; } = string.Empty;

    public string? StackTrace { get; set; } = string.Empty;

    public string ExceptionType { get; set; } = string.Empty;

    public HttpStatusCode StatusCode { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
