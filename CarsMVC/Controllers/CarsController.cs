﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsMVC.ViewModels;

namespace CarsMVC.Controllers
{
    public class CarsController : Controller
    {

        // GET: Car
        //public ActionResult Index()
        //{
        //    var cars = CarsViewModel.GetCars();
        //    return View(cars);
        //}

        public ActionResult Filter()
        {
            return View(new CarsViewModel());
        }

        [HttpPost]
        public ActionResult Filter(CarsViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var cars = filter.GetCarsFiltered();
                return View("Index", cars);
            }
            return View("Filter",filter);

        }

        [HttpPost]
        public ActionResult SearchOwner(string term)
        {
            var res = CarsViewModel.SearchOwners(term);
            return Content(res.ToString(), "application/json");
        }

        public ActionResult Create()
        {
            CarViewModel car = new CarViewModel();
            return View("Edit", car);

        }

        [HttpPost]
        public ActionResult Create(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                car.Update();
                return RedirectToAction("Index");
            }

            return View(car);
        }


        public ActionResult Edit(int id)
        {
            var car = CarViewModel.GetCar(id);
            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                car.Update();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult Delete(int id)
        {

            CarViewModel.DeleteCar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Services(int id, string carString)
        {
            return RedirectToAction("Index","Services", new { id, carString });
        }
    }
}