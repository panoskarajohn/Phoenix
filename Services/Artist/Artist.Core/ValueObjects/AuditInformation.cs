using Ardalis.GuardClauses;

namespace Artist.Core.ValueObjects;

public record AuditInformation
{
    public DateTime CreatedAt { get; }
    public DateTime LastModified { get; }
    public string ModifiedBy { get; }
    
    private AuditInformation(DateTime createdAt, DateTime lastModified, string modifiedBy)
    {
        CreatedAt = createdAt;
        LastModified = lastModified;
        ModifiedBy = modifiedBy;
    }
    
    public static AuditInformation New(string modifiedBy)
        => new(DateTime.UtcNow, DateTime.UtcNow, modifiedBy);

    public static AuditInformation FromExisting(DateTime createdAt, DateTime lastModified, string modifiedBy)
    {   
        Guard.Against.Default(createdAt, nameof(createdAt));
        Guard.Against.Default(lastModified, nameof(lastModified));
        
        return new(createdAt, lastModified, modifiedBy);
    } 
}