using System.ComponentModel.DataAnnotations;

namespace SmartHome.DomainCore.Data.Models
{
    public class PlaceModel : Model
    {
        /// <summary>
        /// Name of the place, e.g. bathroom.
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// True, if the place is inside (a building).
        /// </summary>
        [Required]
        public bool IsInside { get; set; }
        
        /// <summary>
        /// Note about the place.
        /// </summary>
        public string? Note { get; set; }
    }
}