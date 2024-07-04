﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.Data.Entities
{
    public abstract class BaseOrder
    {
        public string OrderId { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
