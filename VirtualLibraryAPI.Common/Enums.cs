using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace VirtualLibraryAPI.Common
{
    /// <summary>
    /// Enumeration for department types
    /// </summary>
    public enum DepartmentType
    {
        Fantasy = 1,
        Adventure,
        Science,
        Romance,
        Horror
    }
    /// <summary>
    /// Enumeration for user types
    /// </summary>
    public enum UserType
    {
        Administrator,
        Client,
        Manager
    }
    /// <summary>
    /// Enumeration for item types
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
        MaxCopiesExceeded,
        ExpiredCopy,
        DbError,
        InternalServerError
    }
}