using System.Collections.Generic;
using Department.DTO;
using Employee.DTO;

namespace Employee.BLL
{
    public interface IEmployeeManagement
    {
    
        EmployeeDetail AddEmployeeDetail(EmployeeDetail employeeDetail);
        void DeleteEmployeeDetail(int accountNumber);
        EmployeeDetail GetSingleEmployeeDetail(int accountNumber);
     
        List<EmployeeDetail> ShowAllEmployeeDetail();
        void UpdateEmployeeDetail(int Id, int Experience, string marriedStatus);
    }
}