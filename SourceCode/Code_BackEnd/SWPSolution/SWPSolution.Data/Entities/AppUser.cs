﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Address> Addresses { get; set; }

        public List<Order> Orders { get; set; }

        public List<PreOrder> PreOrders { get; set; }

        public List<Review> Reviews { get; set; }
    }
}