using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Corron.CarService;

namespace CarsMVC.ViewModels
{
    public class ServiceViewModel
    {
        private ServiceModel _serviceModel;


        public ServiceViewModel()
        {
            _serviceModel = new ServiceModel();
            ServiceLines = new List<ServiceLineViewModel>();
            BlankLine = new ServiceLineViewModel();
        }

        public ServiceViewModel(ServiceModel SM,string forCar)
        {
            _serviceModel = SM;
            ServiceLines = new List<ServiceLineViewModel>();
            foreach (ServiceLineModel SL in _serviceModel.ServiceLineList)
            {
                ServiceLines.Add(new ServiceLineViewModel(SL));
            }
            BlankLine = new ServiceLineViewModel();
            ForCar = forCar;
        }

        [ScaffoldColumn(false)]
        public int ServiceID { get => _serviceModel.ServiceID; set => _serviceModel.ServiceID = value; }
        [ScaffoldColumn(false)]
        public int CarID { get => _serviceModel.CarID; set => _serviceModel.CarID = value; }

        public string ForCar { get; }

        [Display(Name = "Service Date")]
        [DisplayFormat(DataFormatString = "{0:d}",ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get => _serviceModel.ServiceDate; set => _serviceModel.ServiceDate = value; }

        [Display(Name = "Tech Name")]
        [Required]
        public string TechName { get => _serviceModel.TechName; set => _serviceModel.TechName = value; }

        [Display(Name = "Labor Cost")]
        public decimal LaborCost { get => _serviceModel.LaborCost; private set => _serviceModel.LaborCost = value; }

        [Display(Name = "Parts Cost")]
        public decimal PartsCost { get => _serviceModel.PartsCost; private set => _serviceModel.PartsCost = value; }

        [Display(Name = "Total")]
        public decimal TotalCost { get => _serviceModel.TotalCost;}

        public void RecalcCost() { _serviceModel.RecalcCost(); }

        public List<ServiceLineViewModel> ServiceLines { get; set; }

        [ScaffoldColumn(false)]
        public ServiceLineViewModel BlankLine { get; }

        public bool Update(bool recalcOnly=false)
        {

            //copy detail lines to underlying object
            _serviceModel.ServiceLineList.Clear();

            foreach (ServiceLineViewModel SL in ServiceLines)
            {
                if (!SL.Delete)
                    _serviceModel.ServiceLineList.Add(SL.UnderlyingModel);
            }
            RecalcCost();
            if (recalcOnly)
                return true;

            return SQLData.UpdateService(_serviceModel);
        }

        public static void DeleteService(int id)
        {
            SQLData.DeleteService(id);
        }
    }
    public class ServicesViewModel
    {

        public List<ServiceViewModel> Services { get; private set; }
        public string ForCar { get; private set; }
        public int ForCarId { get; private set; }

        private void Add(ServiceModel service, string forCar)
        {
            if (Services is null)
                Services = new List<ServiceViewModel>();

            Services.Add(new ServiceViewModel(service,forCar));
        }

        public static ServiceViewModel GetService(int id)
        {
            ServiceModel SM = SQLData.GetService(id);
            var SVM=new ServiceViewModel(SM,GetCarDescription(SM.CarID));
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
            var servicesVM = new ServicesViewModel { ForCar = GetCarDescription(id), ForCarId=id };

            foreach (var service in services)
            {
                servicesVM.Add(service,servicesVM.ForCar);
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