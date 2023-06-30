namespace VirtualLibraryAPI.Common
{
    /// <summary>
    /// Enum status of vaidation
    /// </summary>
    public enum ValidationStatus
    {
        Valid,
        NotFound,
        DbError,
        InternalServerError,
        InvalidBookingPeriod,
        NotAvailable
    }
}