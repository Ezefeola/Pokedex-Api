namespace Domain.Common.DomainResults;

public class DomainResult : IDomainResult
{
    public bool IsSuccess { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public List<string> Errors { get; private set; } = [];
    public int UpdatedFieldCount { get; private set; }

    protected DomainResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    protected DomainResult(bool isSuccess, List<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public DomainResult WithDescription(string description)
    {
        Description = description;
        return this;
    }   

    public DomainResult WithUpdatedFieldCount(int count)
    {
        UpdatedFieldCount = count;
        return this;
    }

    public static DomainResult Success() => new(true);
    public static DomainResult Failure(List<string> errors) => new(false, errors);
}

public sealed class DomainResult<TValue> : IDomainResult
{
    public bool IsSuccess { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public List<string> Errors { get; private set; } = [];
    public int UpdatedFieldCount { get; private set; }

    private TValue? _value;
    public TValue Value => IsSuccess && _value is not null ? _value : EmptyValue();

    private DomainResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    private DomainResult(TValue value) : this(true)
    {
        _value = value;
    }
    private DomainResult(List<string> errors) : this(false)
    {
        Errors = errors;
    }

    private TValue EmptyValue()
    {
        return _value = default!;
    }

    public DomainResult<TValue> WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public DomainResult<TValue> WithUpdatedFieldCount(int count)
    {
        UpdatedFieldCount = count;
        return this;
    }

    public static DomainResult<TValue> Success(TValue value) => new(value);
    public static DomainResult<TValue> Failure(List<string> errors) => new(errors);
}