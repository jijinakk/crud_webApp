using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crud_webApp.DAL;
using crud_webApp.Models;

namespace crud_webApp.Controllers
{
    public class EmployeeController : Controller
    {

        employee_DAL _employeeDAL = new employee_DAL();

        // GET: Employee
        public ActionResult Index()
        {
            var employeeList = _employeeDAL.GetAllemployee();
            if (employeeList.Count == 0)
            {
                TempData["InfoMessage"] = "currently database is empty";
            }
            return View(employeeList);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var employee = _employeeDAL.GetemployeeByID(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee not Available" + id.ToString();
                    return RedirectToAction("Index");

                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();


            }


        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _employeeDAL.InsertEmployee(employee);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Details saved Successfully";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Save";

                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();


            }

        }


        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _employeeDAL.GetemployeeByID(id).FirstOrDefault();
            if (employee == null)
            {
                TempData["InfoMessage"] = "Employee not Available" + id.ToString();
                return RedirectToAction("Index");

            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProduct(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _employeeDAL.UpdateEmployee(employee);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Details updated Successfully";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product is already available/ubable to update";

                    }
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();


            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _employeeDAL.GetemployeeByID(id).FirstOrDefault();
                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee not Available" + id.ToString();
                    return RedirectToAction("Index");

                }
                return View(employee);


            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();


            }

        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _employeeDAL.DeleteEmployee(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;

                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();


            }

        }
    }
}
