﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KPCOS.Data.Models;

public partial class Employee
{
    public string Id { get; set; }

    public decimal? Salary { get; set; }

    public string SupervisorId { get; set; }

    public string UserId { get; set; }

    public virtual ICollection<Employee> InverseSupervisor { get; set; } = new List<Employee>();

    public virtual ICollection<Project> ProjectConstructionStaffs { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectDesigners { get; set; } = new List<Project>();

    public virtual ICollection<ServiceAssignment> ServiceAssignments { get; set; } = new List<ServiceAssignment>();

    public virtual ICollection<ServiceExecution> ServiceExecutions { get; set; } = new List<ServiceExecution>();

    public virtual Employee Supervisor { get; set; }

    public virtual User User { get; set; }
}