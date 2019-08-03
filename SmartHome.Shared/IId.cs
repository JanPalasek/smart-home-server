namespace SmartHome.Shared
{
    public interface IId<TType>
    {
        TType Id { get; set; }
    }
}