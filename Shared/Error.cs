using System.Text.Json.Serialization;

namespace Shared;

public record Error
{
    public static Error None => new(string.Empty, string.Empty, ErrorType.NONE, null);

    public string Code { get; }

    public string Message { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; }

    public string? InvalidField { get; }

    [JsonConstructor]
    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    // Static Factory Methods
    public static Error NotFound(string? code, string record, Guid id) =>
        new(code ?? "RECORD_NOT_FOUND", $"{record} with id {id} not found", ErrorType.NOT_FOUND);

    public static Error Validation(string? code, string message, string? invalidField = null) =>
        new Error(code ?? "VALIDATION_ERROR", message, ErrorType.VALIDATION, invalidField);

    public static Error Failure(string? code, string message) =>
        new Error(code ?? "FAILURE", message, ErrorType.FAILURE);

    public static Error Conflict(string? code, string message) =>
        new Error(code ?? "CONFLICT", message, ErrorType.CONFLICT);
    public static Error Conflict(string message) =>
        Conflict(null, message);

    public Failure ToFailure() => this;
}

public enum ErrorType
{
    /// <summary>
    /// Exception is NONE
    /// </summary>
    NONE,

    /// <summary>
    /// Exception that occurs during validation
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Exception that occurs when a record is not found
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Exception from server
    /// </summary>
    FAILURE,

    /// <summary>
    /// Exception conflict
    /// </summary>
    CONFLICT,
}