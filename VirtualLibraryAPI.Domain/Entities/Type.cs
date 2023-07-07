using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Enum for types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Type : short
    {
        Book = 1,
        Article = 2,
        Magazine = 3,
        Copy = 4
    }

}
