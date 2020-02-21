using System.Data;
using Department.DTO;
using Employee.DTO;

namespace Employee.DAL
{
    public interface IDepartmentAccess
    {
        void SaveDepartmentDetail(EmployeeDetail employeeDetail);
        DataTable ShowDepartmentDetail();
        void SaveDepartment(DepartmentDetail departmentDetail);
    }
}