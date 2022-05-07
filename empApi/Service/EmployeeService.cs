using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationEmp.Model;

namespace WebApplicationEmp.Service
{


    public interface IEmployeeService
    {
         List<Employee> Get();
        Employee Get(string id);
        void DeleteEmployee(string id);
            void AddEmployee(Employee emp);

    }
        public class EmployeeService:IEmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        //public EmployeeService()
        //{
            

        //}
        public EmployeeService(IEmployeeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);

        }

        public List<Employee> Get()
        {
            List<Employee> employees;
            employees = _employees.Find(emp => true).ToList();
            return employees;
        }

        public Employee Get(string id) =>
            _employees.Find<Employee>(emp => emp.Id == id).FirstOrDefault();

        public void DeleteEmployee(string id)
        {
            _employees.DeleteOne(id);
        }

        public void AddEmployee(Employee emp)
        {
            _employees.InsertOne(emp);
            
        }
    }
}
