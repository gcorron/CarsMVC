﻿using CarsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsMVC.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index(int id, string carString)
        {
            //List<ServiceModel> services = SQLData.GetServices(id);
            ViewBag.carString = carString;
            return View();
        }

        // GET: Services/Details/5
        public ActionResult Details(int id)
        {
            //ServiceModel service = SQLData.GetService(id);
            return View();
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int id)
        {
            //ServiceModel service = SQLData.GetService(id);
            return View();
        }

        // POST: Services/Edit/5
        [HttpPost]
        public ActionResult Edit(ServiceModel service)
        {
            //if (SQLData.UpdateService(service))
                return RedirectToAction("Index",service.CarID);
            //else
            //    return View();
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Services/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
