using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Corron.CarService;

namespace CarsMVC.ViewModels
{
    public class CarsViewModel
    {

        [Required]
        [Display(Name = "Search Year")]
        public string Year { get; set; }
        [Display(Name = "Search Owner")]
        public string Owner { get; set; }
        [Display(Name = "Select a Year")]
        public IEnumerable<SelectListItem> SelectYears { get; private set; }


        public CarsViewModel()
        {

            SelectListItem[] anyYear = { new SelectListItem { Value = "0", Text = "Any Year" } };

            var yearList = SQLData.GetCars()
                           .Select(c => c.Year)
                           .Distinct()
                           .OrderBy(y => y)
                           .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() }).ToList<SelectListItem>();
            yearList.Add(new SelectListItem { Value = "0", Text = "Any Year" });
            SelectYears = (IEnumerable<SelectListItem>)yearList.OrderBy(i => i.Value);
        }

        public static string[]  SearchOwners(string search)
        {
            return SQLData.SearchOwners(search);
        }

        public List<CarViewModel> GetCarsFiltered()
        {
            var cars = SQLData.GetCarsFiltered(Int32.Parse(Year), Owner + "%");
            cars.Sort();
            var carsVM = new List<CarViewModel>();
            foreach (var car in cars)
            {
                CarViewModel carVM = new CarViewModel(car);
                carsVM.Add(carVM);
            }
            return carsVM;
        }
    }

}