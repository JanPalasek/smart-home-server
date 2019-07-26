namespace SmartHome.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Ancestor for all entities that are used in the application.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        public long Id { get; set; }
    }
}