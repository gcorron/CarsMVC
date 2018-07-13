using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Corron.CarService;

namespace CarsMVC.Models
{
    public class CarModel2 : ICarModel
    {

        public int CarID { get; set;}
        public string Make { get;set; }
        public string Model { get;set; }
        public string Owner { get;set; }
        public int Year { get;set; }

        public string this[string columnName] => throw new NotImplementedException();
        public string Error => throw new NotImplementedException();

        public int CompareTo(ICarModel other)
        {
            throw new NotImplementedException();
        }

        public void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }


        public void EndEdit()
        {
            throw new NotImplementedException();
        }
    }
}