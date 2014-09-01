using NorthWindUpdate.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NorthWindUpdate.Models.Concrete
{
    public class EFEmployeeRepository : IEFEmployeeRepository,IDisposable
    {
        private NORTHWNDEntities db = new NORTHWNDEntities();
        public List<Employee> GetAllEmployees()
        {
            var employees = db.Employees.Include("Employee1").ToList();
            return employees;
        }
        public Employee GetEmployeeById(int id)
        {
            Employee employee = db.Employees.Find(id);
            return employee;
        }
        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }
        public void SaveChanges(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool UploadImage(HttpPostedFileBase file, Employee employee,HttpServerUtilityBase server,HttpRequestBase request)
        {
            string selectedImage = System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(
                                   server.MapPath("~/images/EmployeeProfile"), selectedImage);
            // file is uploaded
            try
            {
                file.SaveAs(path);
            }
            catch
            {
                return false;
            }
            StringBuilder imageURL = new StringBuilder();
         
            imageURL.Append("http://");
            imageURL.Append(request.Url.Authority);
            imageURL.Append("/images/EmployeeProfile/");
            imageURL.Append(selectedImage);

            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
                employee.Photo = array;
                employee.PhotoPath = imageURL.ToString();

            }
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        
            return true;//Successful save
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}