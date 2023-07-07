namespace VirtualLibraryAPI.Common
{
    /// <summary>
    /// Enum status of copy vaidation
    /// </summary>
    public enum ValidationCopyStatus
    {
        Valid,
        NotFound,
        DbError,
        InternalServerError,
        InvalidBookingPeriod,
        NotAvailable
    }
    /// <summary>
    /// Enum status of user vaidation
    /// </summary>
    public enum ValidationUserStatus
    {
        Valid,
        UserNotFound,
        MaxCopiesExceeded,
        ExpiredCopy,
        DbError,
        InternalServerError
    }
}