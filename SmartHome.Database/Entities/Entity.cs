using SmartHome.Shared;

namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Ancestor for all entities that are used in the application.
    /// </summary>
    public abstract class Entity : IId<long>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public long Id { get; set; }
    }
}