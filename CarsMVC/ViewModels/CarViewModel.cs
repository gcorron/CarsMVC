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

        public static List<CarViewModel> GetCars()
        {
            var cars = SQLData.GetCars();
            cars.Sort();
            var carsVM = new List<CarViewModel>();
            foreach (var car in cars)
            {
                CarViewModel carVM = new CarViewModel();
                carVM.MapFromDto(car);
                carsVM.Add(carVM);
            }
            return carsVM;            
        }

        public static CarViewModel GetCar(int id)
        {
            var car = SQLData.GetCar(id);
            var carVM = new CarViewModel();
            carVM.MapFromDto(car);
            return carVM;
        }

        public void Update()
        {
            CarModel car = new CarModel();
            this.MapToDto(car);
            SQLData.UpdateCar(car);
        }

        public static void DeleteCar(int id)
        {
            SQLData.DeleteCar(id);
        }


        private void MapFromDto(CarModel car)
        {
            this.CarID = car.CarID;
            this.Year = car.Year;
            this.Make = car.Make;
            this.Model = car.Model;
            this.Owner = car.Owner;
        }

        private void MapToDto(CarModel car)
        {
            car.CarID = this.CarID;
            car.Year = this.Year;
            car.Make = this.Make;
            car.Model = this.Model;
            car.Owner = this.Owner;
        }



    }
}