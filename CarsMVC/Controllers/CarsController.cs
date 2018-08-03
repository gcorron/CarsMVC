using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsMVC.ViewModels;
using Newtonsoft.Json;

namespace CarsMVC.Controllers
{
    public class CarsController : Controller
    {

        public ActionResult Index()
        {
            var cookie = Request.Cookies.Get("filter");
            if (cookie is null || cookie.Value is null)
                return RedirectToAction("Filter");
            else
            {
                string json = cookie.Value;
                CarsViewModel filter = JsonConvert.DeserializeObject<CarsViewModel>(json);
                var cars = filter.GetCarsFiltered();
                return View("Index", cars);
            }
        }

        public ActionResult Filter()
        {
            CarsViewModel filter = new CarsViewModel();
            var cookie = Request.Cookies.Get("filter");
            if (!(cookie is null))
            {
                string json = cookie.Value;
                if (json != "")
                    filter = JsonConvert.DeserializeObject<CarsViewModel>(json);
            }
            filter.PrepYears();
            return View(filter);
        }

        [HttpPost]
        public ActionResult Filter(CarsViewModel filter)
        {
            if (ModelState.IsValid)
            {
                var cars = filter.GetCarsFiltered();

                //remember the filter settings for later
                string json = JsonConvert.SerializeObject(filter);
                var cookie = new HttpCookie("filter",json);
                HttpContext.Response.SetCookie(cookie);
                ViewBag.Search = filter.SearchDesc;
                return View("Index", cars);
            }
            filter.PrepYears();
            return View("Filter",filter);

        }

        [HttpGet]
        public ActionResult SearchOwner(string term)
        {
            var res = CarsViewModel.SearchOwners(term);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            CarViewModel car = CarsViewModel.CreateCar();
            return View("Edit", car);

        }

        [HttpPost]
        public ActionResult Create(CarViewModel car)
        {
            return PostEditCar(car);
        }


        public ActionResult Edit(int id)
        {
            var car = CarViewModel.GetCar(id);
            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(CarViewModel car)
        {
            return PostEditCar(car);
        }

        public ActionResult PostEditCar(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                car.Update();
                var cars = CarsViewModel.GetCarsForOwner(car.Owner);
                return View("Index", cars);
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