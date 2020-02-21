using System.Collections.Generic;
using Department.DTO;
using Microsoft.AspNetCore.Http;

namespace Employee.BLL
{
  public  interface IDepartmentManagement
    {
  
        List<DepartmentDetail> ShowAllDepartmentDetail();
        bool AddDepartment(string Name, IFormFile EmployeeDetails);
    }
}