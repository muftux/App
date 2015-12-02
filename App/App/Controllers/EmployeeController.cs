﻿using App.Models;
using System.Web.Mvc;
using App.Service;
using PagedList;
using System;
using System.Web;
using App.Service.Interfaces;
using CodeFirst;
using System.Collections.Generic;
using Newtonsoft.Json;
using App.Models.JqGridObjects;

namespace App.Controllers
{
    public class EmployeeController : Controller
    {

        private IEmployeeService service;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.service = employeeService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                service.Add(employee);
                return RedirectToAction("ShowEmployees");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ShowEmployees()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult RemoveEmployee(int id)
        {
            EmployeeViewModel employee = service.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RemoveEmployee(EmployeeViewModel employee)
        {
            service.Remove(employee);
            return RedirectToAction("ShowEmployees");
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditEmployee(int id)
        {
            EmployeeViewModel employee = service.GetSingle(id);
            return View(employee);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditEmployee(EmployeeViewModel employee)
        {
            service.Edit(employee);
            return RedirectToAction("ShowEmployees");          
        }

        [HttpGet]
        [Authorize]
        public ActionResult Manage(ManagingRequest request)
        {
            return View(service.GetTableData(request));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public void ApplyChanges(ManagingDateModel model)
        {
            service.ApplyAbsence(model);
        }

        [HttpGet]
        [Authorize]
        public string GetEmployeeData(bool _search, int page, int rows, string sidx, string sord, string filters)
        {
            var toTransfer = JqGridEmployeePagedCollection.Create(_search, page, rows, sidx, sord, filters);       
            return JsonConvert.SerializeObject(toTransfer);
        }
    }
}