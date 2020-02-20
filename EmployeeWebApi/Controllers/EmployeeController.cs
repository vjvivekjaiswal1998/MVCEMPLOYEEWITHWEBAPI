using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Department.DTO;
using Employee.BLL;
using Employee.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        Product[] products = new Product[]
      {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
      };
        //        Product product = new Product();

        //        product.Name = "Apple";
        //product.ExpiryDate = new DateTime(2008, 12, 28);
        //        product.Price = 3.99M;
        //product.Sizes = new string[] { "Small", "Medium", "Large" };

        //    string output = JsonConvert.SerializeObject(product);
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }
        private readonly IEmployeeManagement _employeeManagement;
        private readonly IDepartmentManagement _departmentManagement;
        EmployeeDetail employeeDetail = new EmployeeDetail();
        readonly DepartmentDetail departmentDetail = new DepartmentDetail();
        public EmployeeController(IEmployeeManagement employeeManagement, IDepartmentManagement departmentManagement)
        {
            _employeeManagement = employeeManagement;
            _departmentManagement = departmentManagement;

        }
        [HttpGet]
        public ActionResult<List<string>> ShowEmployeeDetail()
        {
            List<EmployeeDetail> employeeDetail = _employeeManagement.ShowAllEmployeeDetail();
            List<string> all = new List<string>();
            foreach (EmployeeDetail employeeDetails in employeeDetail)
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                new XmlSerializer(employeeDetail.GetType()).Serialize(writer, employeeDetail);
                string xml = writer.ToString();
                all.Add(xml);
            }
            return all;
        }
        [HttpGet("{EmployeeID}")]
        public string GetEmployeeDetail(int EmployeeID)
        {
            EmployeeDetail employeeDetail = _employeeManagement.GetSingleEmployeeDetail(EmployeeID);
            // return employeeDetail;
            System.IO.StringWriter writer = new System.IO.StringWriter();
            new XmlSerializer(employeeDetail.GetType()).Serialize(writer, employeeDetail);
            string xml = writer.ToString();
            return xml;
        }

        [HttpPost]
        public EmployeeDetail AddEmployeeDetail([FromBody]EmployeeDetail employeeDetail)
        {
            if (employeeDetail != null)
            {
                _employeeManagement.AddEmployeeDetail(employeeDetail);
            }
            return employeeDetail;
        }

        [HttpPut("{EmployeeID}")]
        public string UpdateEmployeeDetail(int EmployeeID, [FromBody]EmployeeModel employeeModel)
        {
            _employeeManagement.UpdateEmployeeDetail(EmployeeID, employeeModel.Experience, employeeModel.MarriedStatus);
            return "update successfully";
        }

        [HttpDelete("{EmployeeID}")]
        public string DeleteEmployeeDetail(int EmployeeID)
        {
            _employeeManagement.DeleteEmployeeDetail(EmployeeID);
            return "Deleted successfully";
        }
    }
}