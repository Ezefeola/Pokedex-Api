using System.Net;

namespace Shared.Result;
public sealed class Result : IResult
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
    public object Payload => default!;

    private Result(bool isSuccess, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        HttpStatusCode = statusCode;
    }

    public Result WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Result WithErrors(List<string> errors)
    {
        Errors = errors;
        return this;
    }

    public static Result Success(HttpStatusCode statusCode) => new(true, statusCode);
    public static Result Failure(HttpStatusCode statusCode) => new(false, statusCode);

}


public sealed class Result<TResponse> : IResult<TResponse>
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
    public TResponse? Payload { get; set; }
    private Result(bool isSuccess, HttpStatusCode statusCode)
    {
        IsSuccess = isSuccess;
        HttpStatusCode = statusCode;
        Description = string.Empty;
        Errors = [];
        Payload = default;
    }

    public Result<TResponse> WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Result<TResponse> WithErrors(List<string> errors)
    {
        Errors = errors;
        return this;
    }

    public Result<TResponse> WithPayload(TResponse payload)
    {
        Payload = payload;
        return this;
    }

    public static Result<TResponse> Success(HttpStatusCode statusCode) => new(true, statusCode);
    public static Result<TResponse> Failure(HttpStatusCode statusCode) => new(false, statusCode);
}