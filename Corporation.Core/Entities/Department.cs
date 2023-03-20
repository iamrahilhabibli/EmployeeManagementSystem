﻿using Corporation.Core.Interface;

namespace Corporation.Core.Entities;

public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EmployeeLimit { get; }
    public int CompanyId { get; set; }
    private static int _count { get; set; }

    public Department()
    {
        Id = _count++;
    }
    public Department(string name, int employeelimit, int companyid) : this()
    {
        this.Name = name;
        this.EmployeeLimit = employeelimit;
        this.CompanyId = companyid;
    }
}

