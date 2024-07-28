using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.ViewModels.Sales
{
    public class OrderTrackingUpdateRequest
    {
        public string Note {  get; set; }
        public IFormFile? Image { get; set; }
    }
}
