namespace SmartHome.Database.Entities
{
    /// <summary>
    /// Represents a place.
    /// </summary>
    public class Place : Entity
    {
        /// <summary>
        /// Name of the place, e.g. bathroom.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// True, if the place is inside (a building).
        /// </summary>
        public bool IsInside { get; set; }
        
        /// <summary>
        /// Note about the place.
        /// </summary>
        public string? Note { get; set; }
    }
}