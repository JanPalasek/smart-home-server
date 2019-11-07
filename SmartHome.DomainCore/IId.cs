namespace SmartHome.DomainCore
{
    public interface IId<TType>
    {
        TType Id { get; set; }
    }
}