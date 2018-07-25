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
    public class CarsViewModel : IDataErrorInfo
    {

        [Required]
        [Display(Name = "Lookup Year")]
        public string Year { get; set; }
        [Display(Name = "Lookup Owner")]
        public string Owner { get; set; }
        [Display(Name = "Select a Year")]
        public IEnumerable<SelectListItem> SelectYears { get; private set; }

        public void PrepYears()
        {
            var yearList = SQLData.GetCars()
                           .Select(c => c.Year)
                           .Distinct()
                           .OrderBy(y => y)
                           .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() }).ToList<SelectListItem>();
            yearList.Add(new SelectListItem { Value = "0", Text = "Any Year" });
            SelectYears = (IEnumerable<SelectListItem>)yearList.OrderBy(i => i.Value);
            if (String.IsNullOrWhiteSpace(Year))
                Year = "0";
            else
            {
                if (!(yearList.Select(yl => yl.Value).Contains(Year))) //Year must be in the list
                    Year = "0";
            }
        }

        public static string[]  SearchOwners(string search)
        {
            return SQLData.SearchOwners(search);
        }

        public static CarViewModel CreateCar()
        {
            return new CarViewModel { Year = DateTime.Now.Year - 8 };
        }

        public List<CarViewModel> GetCarsFiltered()
        {
            return GetCars2(SQLData.GetCarsFiltered(Int32.Parse(Year), Owner));
        }

        public static List<CarViewModel> GetCarsForOwner(string owner)
        {
            return GetCars2(SQLData.GetCarsFiltered(0, owner));
        }

        private static List<CarViewModel> GetCars2(List<CarModel> cars)
        {
            cars.Sort();
            var carsVM = new List<CarViewModel>();
            foreach (var car in cars)
            {
                CarViewModel carVM = new CarViewModel(car);
                carsVM.Add(carVM);
            }
            return carsVM;
        }

        public string SearchDesc
        {
            get {
                string owner="anyone";
                string year="any year";
                if (!(String.IsNullOrWhiteSpace(Owner)))
                    owner = Owner;
                if (!(String.IsNullOrWhiteSpace(Year)) && Year.CompareTo("0")>0)
                    year = $"{Year} model year";

                return $"owner '{owner}' for {year}";
            }

        }

        public string Error
        {
            get
            {
                if (Year.CompareTo("0") <= 0 && String.IsNullOrWhiteSpace(Owner))
                    return "Specify the Year and/or Owner";
                else
                    return null;
            }
        }

        public string this[string columnName] {
            get { return null; }
        }

    }

}