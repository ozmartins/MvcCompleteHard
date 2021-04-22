using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.Business.Models
{
    public class Address : Entity
    {
        
        public string Street { get; set; }
        
        public string Number { get; set; }
        
        public string Complement { get; set; }
        
        public string ZipCode { get; set; }
        
        public string Neighborhood { get; set; }
        
        public string CityName { get; set; }
        
        public string State { get; set; }
        
        public Guid SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }

    }
}
