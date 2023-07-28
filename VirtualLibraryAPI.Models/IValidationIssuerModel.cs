using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Validation issuer model interface 
    /// </summary>
    public interface IValidationIssuerModel
    {
        /// <summary>
        ///  Check if client can reserve a copy
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ValidationIssuerStatus CanIssuerIssueCopy(int useriId, int copyId);
    }
}
