using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DbHelper
{
    public interface ISoftDeleted
    {
        bool IsDeleted { get; set; }
    }
}
