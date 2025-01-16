﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    /// <summary>
    /// Used to define the level of supervision/experience that a user/role has.
    /// </summary>
    public enum SupervisoryLevel
    {
        Entry,
        TeamMember,
        TeamLeader,
        Supervisor,
        DepartmentHead,
        Owner
    }
}
