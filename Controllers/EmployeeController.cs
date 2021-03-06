using EmployeeRegistration.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeRegistration.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeDAL employeeDAL = new EmployeeDAL();

        public IActionResult Index()
        {
            List<Employee> empList = new List<Employee>();
            empList = employeeDAL.GetAllEmployee().ToList();
            return View(empList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee objEmp)
        {
            if (ModelState.IsValid)
            {
                employeeDAL.AddEmployee(objEmp);
                return RedirectToAction("Index");
            }

            return View(objEmp);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee emp = employeeDAL.GetEmployeeById(id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind] Employee objEmp)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                employeeDAL.UpdateEmployee(objEmp);
                return RedirectToAction("Index");
            }

            return View(employeeDAL);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee emp = employeeDAL.GetEmployeeById(id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee emp = employeeDAL.GetEmployeeById(id);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            employeeDAL.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
