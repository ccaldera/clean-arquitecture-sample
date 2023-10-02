namespace ScalableTeams.HumanResourcesManagement.Domain.Entities;

public class Employee
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string LastName { get; init; }

    public Guid? DepartmentId { get; set; }
    public required Department Department { get; init; }

    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }

    public List<Employee> Subordinates { get; set; } = new List<Employee>();
}
