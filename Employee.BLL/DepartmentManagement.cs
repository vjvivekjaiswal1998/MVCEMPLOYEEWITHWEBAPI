using ClosedXML.Excel;
using Department.DTO;
using Employee.DAL;
using Employee.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Employee.BLL
{
    public class DepartmentManagement : IDepartmentManagement
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IDepartmentAccess _iDepartmentAccess;
        public DepartmentManagement(IDepartmentAccess iDepartmentAccess)
        {
            _iDepartmentAccess = iDepartmentAccess;
            log.Info("Hello logging world!");
        }
        private static DataTable dataTable = new DataTable();
        public List<DepartmentDetail> ShowAllDepartmentDetail()
        {
            dataTable = _iDepartmentAccess.ShowDepartmentDetail();
            var DepartmentData = new List<DepartmentDetail>();
            foreach (DataRow row in dataTable.Rows)
            {
                DepartmentDetail DepartmentDTO = new DepartmentDetail()
                {
                    DepartmentId = int.Parse(row[StringUtilityBLL.DepartmentIdfield.ToString()].ToString()),
                    DepartmentName = row[StringUtilityBLL.DepartmentNamefield.ToString()].ToString(),
                };
                DepartmentData.Add(DepartmentDTO);
            }
            log.Info("Display all Employee detail");
            return DepartmentData;
        }
        //public bool AddDepartment(string Name, IFormFile EmployeeDetails)
        //{
        //    string filepath;
        //    if (EmployeeDetails.Length > 0)
        //    {
        //        filepath = "Files";
        //        var stream = new FileStream(filepath, FileMode.Create);
        //        EmployeeDetails.CopyTo(stream);
        //        var saved = File.Create("wwwroot\\Files\\Temp.ods");
        //        stream.Seek(0, SeekOrigin.Begin);
        //        stream.CopyTo(saved);
        //    }
        //    return true;
        //    //  return _iDepartmentAccess.NewDepartment(Name);
        //   // return _iDepartmentAccess.SaveDepartmentDetail();
        //}
        public bool AddDepartment(string DepartmentName, IFormFile EmployeeDetails)
            
        { DepartmentDetail departmentDetail = new DepartmentDetail();
            departmentDetail.DepartmentName = DepartmentName;
            _iDepartmentAccess.SaveDepartment(departmentDetail);
            Stream data = EmployeeDetails.OpenReadStream();
            using (XLWorkbook workbook = new XLWorkbook(data))

            {
                IXLWorksheet worksheet = workbook.Worksheet(1);
                EmployeeDetail employee = new EmployeeDetail();
                bool Row = true;
                foreach (IXLRow row in worksheet.Rows())
                {
                    if(Row )
                    {
                        Row = false;

                    }
                    else
                    {
                        employee.EmployeeNumber = int.Parse (row.Cell(1).Value.ToString());
                        employee.EmployeeName = row.Cell(2).Value.ToString();
                        employee.DOB= Convert.ToDateTime(row.Cell(3).Value.ToString());
                        employee.Experience = int.Parse( row.Cell(4).Value.ToString());
                        employee.Gender= row.Cell(5).Value.ToString();
                        employee.MarriedStatus= row.Cell(6).Value.ToString();
                        employee.DepartmentId = int.Parse( row.Cell(7).Value.ToString());
                        _iDepartmentAccess.SaveDepartmentDetail(employee);
                            log.Info("Department Detail Addedd Successfully");
                    }
                }

            }
            return true;
        }
    }
}

