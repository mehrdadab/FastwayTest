using System;
namespace NorthWindUpdate.Models.Abstract
{
    public interface IEFEmployeeRepository
    {
        void AddEmployee(NorthWindUpdate.Models.Employee employee);
        
        System.Collections.Generic.List<NorthWindUpdate.Models.Employee> GetAllEmployees();
       
        NorthWindUpdate.Models.Employee GetEmployeeById(int id);
        
        void SaveChanges(NorthWindUpdate.Models.Employee employee);
     
        bool UploadImage(System.Web.HttpPostedFileBase file, NorthWindUpdate.Models.Employee employee, System.Web.HttpServerUtilityBase server, System.Web.HttpRequestBase request);
    }
}
