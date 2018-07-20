using CarsMVC.ViewModels;
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
        public ActionResult Index(int id)
        {
            var services = ServicesViewModel.GetServices(id);
            return View("Index",services);
        }

        // GET: Services/Details/5
        public ActionResult Details(int id)
        {
            return View(ServicesViewModel.GetService(id));
        }

        // GET: Services/Create
        public ActionResult Create(int id)
        {
            return View("Edit",ServicesViewModel.CreateService(id));
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ServicesViewModel.GetService(id));
        }

        // POST: Services/Edit/5
        [HttpPost]
        public ActionResult Edit(ServiceViewModel service)
        {
            RemoveBlankLine();
            if (ModelState.IsValid)
            {
                try
                {
                    service.Update();
                    return RedirectToAction("Details", new { id = service.ServiceID });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("CustomError", e.Message);
                    return View(service);
                }

            }
            return View(service);
         }

        [HttpPost]
        public ActionResult Recalc(ServiceViewModel service)
        {
            RemoveBlankLine();
            if (ModelState.IsValid)
            {
                service.Update(true);
            }
            return View("Edit",service);
        }

        [HttpPost]
        public ActionResult Delete(ServiceViewModel service)
        {
            // don't bother validating for delete!
            int CarID = service.CarID;
            try
            {
                ServicesViewModel.DeleteService(service.ServiceID);
                return Index(CarID);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("CustomError",e.Message);
                return View("Edit", service);
            }

        }


        private void RemoveBlankLine()
        {
            ModelState.Remove("BlankLine.ServiceLineType");
            ModelState.Remove("BlankLine.ServiceLineDesc");
            ModelState.Remove("BlankLine.ServiceLineCharge");
            ModelState.Remove("BlankLine.ServiceLineDelete");
        }

    }
}
