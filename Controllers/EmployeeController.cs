using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_CRUD_Operation_Practice.Models;
using MVC_CRUD_Operation_Practice.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVC_CRUD_Operation_Practice.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL context = new EmployeeDAL();
        private readonly EmployeeDBContext _context;

        public EmployeeController(EmployeeDBContext context)
        {
            _context = context;
        }
        // GET: EmployeeController
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employee.ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> AddOrEdit(int? employeeId)
        {
            ViewBag.PageName = employeeId == null ? "Create Employee" : "Edit Employee";
            ViewBag.IsEdit = employeeId == null ? false : true;
            if (employeeId == null)
            {
                return View();
            }
            else
            {
                var employee = await _context.Employee.FindAsync(employeeId);

                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int employeeId, [Bind("EmployeeId,EmployeeName,EmployeeSalary,EmployeeAddress,EmployeeRole,EmployeeCity")] Employee employeeData)

        {
            bool IsEmployeeExist = false;

            Employee employee = await _context.Employee.FindAsync(employeeId);

            if (employee != null)
            {
                IsEmployeeExist = true;
            }
            else
            {
                employee = new Employee();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employee.EmployeeName = employeeData.EmployeeName;
                    employee.EmployeeSalary = employeeData.EmployeeSalary;
                    employee.EmployeeAddress = employeeData.EmployeeAddress;
                    employee.EmployeeRole = employeeData.EmployeeRole;
                    employee.EmployeeCity = employeeData.EmployeeCity;

                    if (IsEmployeeExist)
                    {
                        _context.Update(employee);
                    }
                    else
                    {
                        _context.Add(employee);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeData);
        }

        // Employee Details
        public async Task<IActionResult> Details(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FirstOrDefaultAsync(m => m.EmployeeId == employeeId);

            return View(employee);
        }

        // POST: Employees/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int employeeId)
        {
            var employee = await _context.Employee.FindAsync(employeeId);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
