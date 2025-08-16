namespace Domain.Common.DomainResults;
public interface IDomainResult
{
    public bool IsSuccess { get; }
    public string Description { get; }
    public List<string> Errors { get; }
    public int UpdatedFieldCount { get; }
}