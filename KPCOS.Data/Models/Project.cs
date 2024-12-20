﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPCOS.Data.Models;

public partial class Project
{
    public Project()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "Actual Cost is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Actual Cost must be a positive value.")]
    public decimal? ActualCost { get; set; }

    public string ConstructionStaffId { get; set; }
    [Required(ErrorMessage = "Customer is required.")]

    public string CustomerId { get; set; }
    [Required(ErrorMessage = "Customer is required.")]

    public string DesignerId { get; set; }
    [Required(ErrorMessage = "End Date is required.")]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "EstimateCost is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "EstimateCost must be a positive value.")]
    public decimal? EstimatedCost { get; set; }

    [Required(ErrorMessage = "Start Date is required.")]
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; }

    public virtual Employee ConstructionStaff { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<DesignConcept> DesignConcepts { get; set; } = new List<DesignConcept>();

    public virtual Employee Designer { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}