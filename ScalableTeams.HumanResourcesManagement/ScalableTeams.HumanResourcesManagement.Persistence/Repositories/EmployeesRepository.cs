﻿using Microsoft.EntityFrameworkCore;
using ScalableTeams.HumanResourcesManagement.Domain.Entities;
using ScalableTeams.HumanResourcesManagement.Domain.Repositories;

namespace ScalableTeams.HumanResourcesManagement.Persistence.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly HumanResourcesManagementContext dbContext;

    public EmployeesRepository(HumanResourcesManagementContext context)
    {
        this.dbContext = context;
    }

    public async Task<Employee?> GetEmployeeAndManagerByEmployeeId(Guid id)
    {
        return await dbContext
            .Employees
            .Include(x => x.Manager)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}