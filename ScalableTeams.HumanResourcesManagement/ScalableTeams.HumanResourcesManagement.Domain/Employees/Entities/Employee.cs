using ScalableTeams.HumanResourcesManagement.Domain.Common.Entitites;
using ScalableTeams.HumanResourcesManagement.Domain.Departments.Entities;

namespace ScalableTeams.HumanResourcesManagement.Domain.Employees.Entities;

public class Employee : Entity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }

    public Guid? DepartmentId { get; set; }
    public required Department Department { get; init; }

    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public List<Employee> Subordinates { get; set; } = new List<Employee>();
}
