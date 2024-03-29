﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Magazine model interface 
    /// </summary>
    public interface IMagazineModel
    {
        /// <summary>
        /// Method for get all magazines
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Magazine> GetAllMagazines();
        /// <summary>
        /// Method for get magazine by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine GetMagazineById(int id);
        /// <summary>
        /// Method for add magazine
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine AddMagazine(Domain.DTOs.Magazine magazine);
        /// <summary>
        /// Method for add copy of a magazine
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Copy AddCopyOfMagazineById(int id, bool isAvailable);
        /// <summary>
        /// Method for update magazine
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine UpdateMagazine(int id, Domain.DTOs.Magazine magazine);
        /// <summary>
        /// Method for delete magazine
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine DeleteMagazine(int id);
        /// <summary>
        /// Get  magazine by id for response DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine GetMagazineByIdResponse(int id);
        /// <summary>
        /// Get all magazines for response DTO
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Magazine> GetAllMagazinesResponse();
        /// <summary>
        /// Add copy to magazine for response
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.DTOs.Magazine AddCopyOfMagazineByIdResponse(int id);
    }
}
