namespace SmartHome.DomainCore.Data
{
    public enum GroupByType
    {
        /// <summary>
        /// Group by every day of a year.
        /// </summary>
        DayOfYear,
        /// <summary>
        /// Group by each month of a year.
        /// </summary>
        Month,
        /// <summary>
        /// Group by a year.
        /// </summary>
        Year,
        /// <summary>
        /// Group by a place.
        /// </summary>
        Place,
    }
}