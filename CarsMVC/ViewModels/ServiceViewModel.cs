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
        private ServiceLines _serviceLines;

        public ServiceViewModel()
        {
            _serviceModel = new ServiceModel();
            _serviceLines = new ServiceLines(_serviceModel.ServiceLineList);
        }

        public ServiceViewModel(ServiceModel SM)
        {
            _serviceModel = SM;
            _serviceLines = new ServiceLines(_serviceModel.ServiceLineList);
        }

        public int ServiceID { get => _serviceModel.ServiceID; set => _serviceModel.ServiceID = value; }
        public int CarID { get => _serviceModel.CarID; set => _serviceModel.CarID = value; }

        [Display(Name = "Service Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
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


        //detail lines wrapper
        public ServiceLines ServiceLines { get => _serviceLines;}

        public static List<ServiceViewModel> GetServices(int CarID)
        {
            var services = SQLData.GetServices(CarID);
            services.Sort();
            var servicesVM = new List<ServiceViewModel>();
            foreach (var service in services)
            {
                servicesVM.Add(new ServiceViewModel(service));
            }
            return servicesVM;
        }

        public static ServiceViewModel GetService(int id)
        {
            return  new ServiceViewModel(SQLData.GetService(id));
        }

        public void Update()
        {
            SQLData.UpdateService(_serviceModel);
        }

        public static void DeleteService(int id)
        {
            SQLData.DeleteService(id);
        }
    }
}