using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarsMVC.Models
{
    public class ServiceLineModel
    {
        [Key]
        public int ServiceLineID { get; set; }

        public byte ServiceLineOrder { get; set; }

        public enum LineTypes : byte { Labor, Parts };

        [DisplayName("Type")]
        public ServiceLineModel.LineTypes ServiceLineType { get; set; }

        [DisplayName("Type")]
        public string ServiceLineTypeString {
            get => new[] { "Labor", "Parts" }[(int)ServiceLineType];
        }

        [Required(ErrorMessage = "A Description is required")]
        [DisplayName("Descr.")]
        public string ServiceLineDesc { get; set; }

        [Range(0.01, 100000)]
        [DisplayName("Charge")]
        public decimal ServiceLineCharge { get; set; }

        [DisplayName("Delete")]
        public bool Delete { get; set; }
    }

    public class ServiceModel
    {
        public ServiceModel()
        {
            ServiceLines = new List<ServiceLineModel>();
        }

        [ScaffoldColumn(false)]
        [Key]
        public int ServiceID { get; set; }

        [ScaffoldColumn(false)]
        public int CarID { get; set; }

        [Required(ErrorMessage = "TechName is a required field.")]
        public string TechName { get; set; }

        [Required(ErrorMessage = "Service Date is a required field.")]
        public DateTime ServiceDate { get; set; }

        public decimal LaborCost { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalCost { get => LaborCost + PartsCost; }

        public List<ServiceLineModel> ServiceLines { get; }
        
    }

    public class CarModel
    {
        [ScaffoldColumn(false)]
        [Key]
        public int CarID { get; set; }

        [Range(1900, 2050)]
        public int Year { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Owner { get; set; }
    }

}