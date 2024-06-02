﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderStatus
    {
        New = 1,
        Preparing = 2,
        Prepared = 3,
        OnTheWay = 4,
        Delivered = 5,
        Canceled = 6
    }
}
