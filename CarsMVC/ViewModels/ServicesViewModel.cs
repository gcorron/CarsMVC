using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Corron.CarService;

namespace CarsMVC.ViewModels
{
    public class ServicesViewModel
    {

        public List<ServiceViewModel> Services { get; private set; }
        public string ForCar { get; private set; }
        public int ForCarId { get; private set; }

        private void Add(ServiceModel service, string forCar)
        {
            if (Services is null)
                Services = new List<ServiceViewModel>();

            Services.Add(new ServiceViewModel(service, forCar));
        }

        public static ServiceViewModel GetService(int id)
        {
            ServiceModel SM = SQLData.GetService(id);
            var SVM = new ServiceViewModel(SM, GetCarDescription(SM.CarID));
            return SVM;
        }

        public static ServiceViewModel CreateService(int id)
        {
            ServiceModel SM = new ServiceModel(id);
            var SVM = new ServiceViewModel(SM, GetCarDescription(SM.CarID));
            SVM.ServiceDate = DateTime.Now;
            return SVM;
        }

        public static ServicesViewModel GetServices(int id)
        {
            var services = SQLData.GetServices(id);
            services.Sort();
            var servicesVM = new ServicesViewModel { ForCar = GetCarDescription(id), ForCarId = id };

            foreach (var service in services)
            {
                servicesVM.Add(service, servicesVM.ForCar);
            }
            return servicesVM;
        }

        public static bool DeleteService(int id)
        {
            return SQLData.DeleteService(id);
        }

        private static string GetCarDescription(int CarID)
        {
            CarModel car = SQLData.GetCar(CarID);
            return car.ToString;
        }

    }
}