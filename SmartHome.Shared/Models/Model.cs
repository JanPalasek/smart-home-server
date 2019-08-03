namespace SmartHome.Shared.Models
{
    public class Model : IId<long?>
    {
        public virtual long? Id { get; set; }
    }
}