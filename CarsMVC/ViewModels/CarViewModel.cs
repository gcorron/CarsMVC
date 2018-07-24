using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Corron.CarService;

namespace CarsMVC.ViewModels
{
    public class CarViewModel
    {
        private CarModel _carModel;
        public CarViewModel()
        {
            _carModel = new CarModel();
        }

        public CarViewModel(CarModel car)
        {
            _carModel = car;
        }

        public int CarID { get => _carModel.CarID; set => _carModel.CarID = value; }

        [Range(1900,2050)]
        public int Year { get => _carModel.Year; set => _carModel.Year = value; }

        [Required]
        public string Make { get => _carModel.Make; set => _carModel.Make = value; }

        [Required]
        public string Model { get => _carModel.Model; set => _carModel.Model = value; }

        [Required]
        public string Owner { get => _carModel.Owner; set => _carModel.Owner = value; }

        public string Description
        {
            get
            {
                return $"{Owner}'s {Year} {Make} {Model}";
            }
        }


        public static CarViewModel GetCar(int id)
        {
            return new CarViewModel(SQLData.GetCar(id));
        }

        public void Update()
        {
            SQLData.UpdateCar(_carModel);
        }

        public static void DeleteCar(int id)
        {
            SQLData.DeleteCar(id);
        }
    }
}