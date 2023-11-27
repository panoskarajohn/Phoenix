using Artist.Core.Enum;
using Community.Exception;

namespace Artist.Core.Exceptions;

public class StatusInvalidStateException : PhoenixExceptions
{
    public StatusInvalidStateException(IEnumerable<Enumeration> artistStatusEnumerable) 
        : base($"Possible values for Artist Status: {String.Join(",", artistStatusEnumerable.Select(s => s.Name))}")
    {
    }
}