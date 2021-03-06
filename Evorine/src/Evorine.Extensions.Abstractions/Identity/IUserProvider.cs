﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public interface IUserProvider
    {
        IApplicationUser GetUser(int id);

        IEnumerable<IApplicationUser> GetUsers();
    }
}
