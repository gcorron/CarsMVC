using CarsMVC.Models;
using Corron.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CarsMVC.Controllers
{
    public class CarsController : Controller
    {

        CarServiceContext JoesDB = new CarServiceContext();

        // GET: Car
        public ActionResult Index()
        {
            var cars = from car in JoesDB.Cars
                       orderby car.Year, car.Make, car.Model
                       select car;

            return View(cars.ToList());
        }

        public ActionResult Create()
        {
            CarModel car = new CarModel();
            return View("Edit", car);

        }

        [HttpPost]
        public ActionResult Create(CarModel car)
        {
            if (ModelState.IsValid)
            {
                //if (SQLData.UpdateCar(car))
                    return RedirectToAction("Index");
            }

            return View(car);
        }


        public ActionResult Edit(int id)
        {
            CarModel car = JoesDB.Cars.Where<CarModel>(c => c.CarID==id).Single<CarModel>();

            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(CarModel car)
        {
            if (ModelState.IsValid)
            {
                JoesDB.Entry(car).State = EntityState.Modified;
                JoesDB.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            //SQLData.DeleteCar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Services(int id, string carString)
        {
            return RedirectToAction("Index","Services", new { id, carString });
        }

    }
}