﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    public enum  UserTypes
    {
        Administrator = 0,
        Client = 1,
        Manager = 2
    }
}