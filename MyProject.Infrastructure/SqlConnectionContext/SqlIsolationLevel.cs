using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Infrastructure
{
    public enum SqlIsolationLevel
    {
        Default,
        ReadCommitted,
        Snapshot,
        ReadUncommitted
    }
}
