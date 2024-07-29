
using EmployeeManagement.DataAccess;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepo : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepo(AppDbContext context)
        {
            this.context = context;
        }

        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            context.Employees.Update(employeeChanges);
            context.SaveChanges();

            return employeeChanges;
        }
    }
}
