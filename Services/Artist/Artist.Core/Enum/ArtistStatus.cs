using Artist.Core.Exceptions;

namespace Artist.Core.Enum;

public class ArtistStatus : Enumeration
{
    public static ArtistStatus Draft = new (1, nameof(Draft).ToLowerInvariant());
    public static ArtistStatus Active = new (2, nameof(Active).ToLowerInvariant());
    public static ArtistStatus Disabled = new (3, nameof(Disabled).ToLowerInvariant());
    public static ArtistStatus Hot = new (4, nameof(Hot).ToLowerInvariant());
    
    private ArtistStatus(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<ArtistStatus> List() => new[] {Draft, Active, Disabled, Hot};

    public static ArtistStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s 
                => string
                    .Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
        
        if (state == null)
        {
            throw new StatusInvalidStateException(List());
        }
        
        return state;
    }
    
    public static ArtistStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state is null)
        {
            throw new StatusInvalidStateException(List());
        }

        return state;
    }
}