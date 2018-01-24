public struct VO_Priority
{
    /// <summary>
    /// Priority order of price. Valid values are: 1 through 4.
    /// </summary>
    public int IntPrice { get; set; }
    /// <summary>
    /// Priority order of direct flights. Valid values are: 1 through 4.
    /// </summary>
    public int IntDirectFlights { get; set; }
    /// <summary>
    /// Priority order of departure time. Valid values are: 1 through 4.
    /// </summary>
    public int IntTime { get; set; }
    /// <summary>
    /// Priority order of specified carrier. Valid values are: 1 through 4.
    /// </summary>
    public int IntVendor { get; set; }

    /// <summary>
    /// Validates wheter all the priorities were specified
    /// </summary>
    /// <returns></returns>
    public bool isValid()
    {
        //All the priorities must be specified
        if (IntPrice < 1 || IntDirectFlights < 1 || IntTime < 1 || IntVendor < 1)
        {
            return false;
        }
        return true;
    }
}