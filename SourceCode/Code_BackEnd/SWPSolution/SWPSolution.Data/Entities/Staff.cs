﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWPSolution.Data.Entities;

public partial class Staff
{
    public string StaffId { get; set; }

    public string Role { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<TrackingPreorder> TrackingPreorders { get; set; } = new List<TrackingPreorder>();

    public virtual ICollection<Trackingorder> Trackingorders { get; set; } = new List<Trackingorder>();
}