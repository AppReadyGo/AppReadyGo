﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppReadyGo.Core.Queries.Users
{
    public class GetUserRolesQuery : IQuery<IEnumerable<StaffRole>>
    {
        public int Id { get; private set; }

        public GetUserRolesQuery(int id)
        {
            this.Id = id;
        }
    }
}
