using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace autosalon_.Data
{
    public class Salon
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int limtCars { get; set; }
        public List<Car> cars { get; set; } = new List<Car>();

    }
    public class Car
    {
        
        public Salon salon { get; set; }
        public int Id { get; set; }
        [Required]
        public string markname { get; set; }
        public string color { get; set; } 
    }
    public class Bmw : Car
    {
        public Bmw()
        {
            markname = "BMW";
        }
    }
    public class Mercedes : Car
    {
        public Mercedes()
        {
            markname = "Mercedes";
        }
    }
}

