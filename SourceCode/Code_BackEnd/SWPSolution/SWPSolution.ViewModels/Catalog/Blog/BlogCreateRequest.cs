﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPSolution.ViewModels.Catalog.Blog
{
    public class BlogCreateRequest
    {

        public string Title { get; set; }

        public string Content { get; set; }

        public string Categories { get; set; }


    }
}
