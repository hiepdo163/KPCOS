﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KPCOS.Data.Models;

public partial class Package
{
    public string Id { get; set; }

    public DateTime? CreateDate { get; set; }

    public string Description { get; set; }

    public int? DiscountPercentage { get; set; }

    public string Duration { get; set; }

    public string FeatureInclude { get; set; }

    public bool? IsActive { get; set; }

    public string Name { get; set; }

    public decimal? Price { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}