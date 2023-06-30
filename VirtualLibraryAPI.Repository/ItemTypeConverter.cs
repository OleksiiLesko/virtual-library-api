using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Repository
{
    /// <summary>
    /// Convert items in string format
    /// </summary>
    public class ItemTypeConverter
    {
        /// <summary>
        /// Get item type in string format
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetItemType(Domain.DTOs.Type type)
        {
            switch (type)
            {
                case Domain.DTOs.Type.Book:
                    return "Book";
                case Domain.DTOs.Type.Article:
                    return "Article";
                case Domain.DTOs.Type.Magazine:
                    return "Magazine";
                default:
                    return string.Empty;
            }
        }
    }
}
