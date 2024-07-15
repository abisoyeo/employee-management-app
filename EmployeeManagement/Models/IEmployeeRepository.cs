namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int Id);
        Employee Add(Employee employee);
    }
}
