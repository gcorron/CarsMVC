using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Corron.CarService;

namespace CarsMVC.Controllers
{
    public class CarsController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            List<CarModel> cars = SQLData.GetCars();
            return View(cars);
        }

        public ActionResult Edit(int id)
        {
            CarModel car = SQLData.GetCar(id);
            return View(car);
        }

        [HttpPost]
        public ActionResult Edit(CarModel car)
        {
            if (ModelState.IsValid)
            {
                if (SQLData.UpdateCar(car))
                    return RedirectToAction("Index");
            }
            return View(car);
        }

        public ActionResult Delete(int id)
        {
            SQLData.DeleteCar(id);
            return RedirectToAction("Index");
        }


    }
}