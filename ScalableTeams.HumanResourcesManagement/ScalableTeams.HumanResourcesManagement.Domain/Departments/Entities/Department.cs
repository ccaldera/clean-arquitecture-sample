using ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;
using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Departments.Entities;

public class Department : Entity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }

    public List<Employee> Members { get; set; } = [];
}
