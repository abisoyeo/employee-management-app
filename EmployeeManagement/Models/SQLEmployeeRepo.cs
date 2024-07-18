
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
            context.EmployeeTest.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.EmployeeTest.Find(id);
            if (employee != null)
            {
                context.EmployeeTest.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.EmployeeTest;
        }

        public Employee GetEmployee(int Id)
        {
            return context.EmployeeTest.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            context.EmployeeTest.Update(employeeChanges);
            context.SaveChanges();

            return employeeChanges;
        }
    }
}
