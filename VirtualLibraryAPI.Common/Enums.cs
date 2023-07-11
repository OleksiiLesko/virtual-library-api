using System.ComponentModel;
using System.Runtime.Serialization;

namespace VirtualLibraryAPI.Common
{
    /// <summary>
    /// Enumeration for user types
    /// </summary>
    public enum UserType
    {
        Administrator,
        Client
    }
    /// <summary>
    /// Type DTO
    /// </summary>
    public enum Type
    {
        Book = 1,
        Article = 2,
        Magazine = 3,
        Copy = 4
    }
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
        UserNotAdministrator,
        MaxCopiesExceeded,
        ExpiredCopy,
        DbError,
        InternalServerError
    }
}