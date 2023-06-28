namespace VirtualLibraryAPI.Common
{
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