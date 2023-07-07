using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Validation user model interface 
    /// </summary>
    public interface IValidationUserModel
    {
        /// <summary>
        ///  Check if user can reserve a copy
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ValidationUserStatus CanUserReserveCopy(int userId);
    }
}
