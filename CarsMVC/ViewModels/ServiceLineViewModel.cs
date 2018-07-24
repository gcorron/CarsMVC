using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Corron.CarService;

namespace CarsMVC.ViewModels
{
    public class ServiceLineViewModel
    {
        private ServiceLineModel _SL;

        public ServiceLineViewModel()
        {
            _SL = new ServiceLineModel();
        }

        public ServiceLineViewModel(ServiceLineModel sl)
        {
            _SL = sl;
        }

        [Display(Name = "Line Type")]
        public ServiceLineModel.LineTypes ServiceLineType { get => _SL.ServiceLineType; set => _SL.ServiceLineType = value; }

        [Display(Name = "Description")]
        [Required]
        public string ServiceLineDesc { get => _SL.ServiceLineDesc; set => _SL.ServiceLineDesc = value; }

        [Display(Name = "Amount")]
        [Range(.01,1_000_000)]
        [DataType(DataType.Currency)]
        public decimal ServiceLineCharge { get => _SL.ServiceLineCharge; set => _SL.ServiceLineCharge = value; }

        public bool Delete { get => _SL.Delete; set => _SL.Delete = value; }

        [Display(Name = "Line Type")]
        public string ServiceLineTypeString { get => _SL.ServiceLineTypeString; }

        [ScaffoldColumn(false)]
        public ServiceLineModel UnderlyingModel { get => _SL; }
    }
}