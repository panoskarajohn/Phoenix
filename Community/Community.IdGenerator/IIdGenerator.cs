namespace Community.IdGenerator;

public interface IIdGenerator<out TId>
{
    TId New();
}