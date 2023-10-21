using ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Departments.Entities;

public class Department
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }

    public List<Employee> Members { get; set; } = new List<Employee>();
}
