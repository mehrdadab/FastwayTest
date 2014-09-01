using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWindUpdate.Models;
using System.IO;
using System.Text;
using NorthWindUpdate.Models.Concrete;

namespace NorthWindUpdate.Controllers
{
    public class EmployeeController : Controller
    {
        EFEmployeeRepository repository = new EFEmployeeRepository();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var employees = repository.GetAllEmployees();
            return View(employees);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = repository.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

    
        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = repository.GetEmployeeById(id);
            if (employee == null)
            {
                TempData["EmployeeErrorMessage"] = "There is no employee available with this id";
                return RedirectToAction("index");
            }
           
            var employees = repository.GetAllEmployees();

            ViewBag.ReportsTo = new SelectList(employees, "EmployeeID", "LastName", employee.ReportsTo);
          
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.SaveChanges(employee);
              
                return RedirectToAction("Index");
            }
            var employees = repository.GetAllEmployees();
         
            ViewBag.ReportsTo = new SelectList(employees, "EmployeeID", "LastName", employee.ReportsTo);
           
            return View(employee);
        }

 
        [HttpGet]
        [ChildActionOnly]
        public ActionResult FindEmployeeById()//A partial view that works as search window
        {
            return PartialView();
        }
        //
        //POST: /Employee/FindEmployeeById/2
        [HttpPost]
        public ActionResult FindEmployeeById(int employeeId)
        {
            return RedirectToAction("Edit", new { id = employeeId });
        }
    
        //Use this action for uploading a new image when needed to update images
        [HttpGet]
        public ActionResult UploadImage(int id)
        {
            ViewBag.EmployeeID = id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file,int id)
        {
            bool isSuccessfulSave = false;
           
            Employee employee = repository.GetEmployeeById(id);

            if (file != null && employee!=null)
            {
              isSuccessfulSave=  repository.UploadImage(file, employee,Server,Request);
            }
         
            if (isSuccessfulSave == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // If couldn't find file or employee
                TempData["EmployeeErrorMessage"] = "The file is not uploaded or employee id is not correct";
                return RedirectToAction("ShowMessage");
            }
        }

        //An action that show Error messages when needed
        public ActionResult ShowMessage()
        {
            return View();
        }
 
        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
          
            base.Dispose(disposing);
        }
    }
}