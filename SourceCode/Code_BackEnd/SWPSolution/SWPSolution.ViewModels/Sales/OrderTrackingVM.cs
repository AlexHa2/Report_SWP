using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.ViewModels.Sales
{
    public class OrderTrackingVM
    {
        public string TrackingId { get; set; }
        public string OrderId { get; set; }
        public string StaffId { get; set; }
        public DateTime? TrackingDate { get; set; }
        public string Note { get; set; }
    }
}
