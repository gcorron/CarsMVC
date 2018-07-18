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
        }

        public ServiceViewModel(ServiceModel SM)
        {
            _serviceModel = SM;
        }

        public ServiceLineViewModel AddServiceLine()
        {
            var SL = new ServiceLineModel();
            _serviceModel.ServiceLineList.Add(SL);
            return new ServiceLineViewModel(SL);
        }


        public IEnumerable<ServiceLineViewModel> ServiceLineList
        {
            get
            {
                foreach (var SL in _serviceModel.ServiceLineList)
                {
                    yield return new ServiceLineViewModel(SL);
                }
            }
        }

        public ServiceLineModel DummyLine { get; set; }



        public int ServiceID { get => _serviceModel.ServiceID; set => _serviceModel.ServiceID = value; }
        public int CarID { get => _serviceModel.CarID; set => _serviceModel.CarID = value; }

        public DateTime ServiceDate { get => _serviceModel.ServiceDate; set => _serviceModel.ServiceDate = value; }

        [Required]
        public string TechName { get => _serviceModel.TechName; set => _serviceModel.TechName = value; }

        public decimal LaborCost { get => _serviceModel.LaborCost; private set => _serviceModel.LaborCost = value; }
        public decimal PartsCost { get => _serviceModel.PartsCost; private set => _serviceModel.PartsCost = value; }
        public decimal TotalCost { get => _serviceModel.TotalCost;}

        public void RecalcCost() { _serviceModel.RecalcCost(); }


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