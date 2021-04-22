using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.Business.Models
{
    public class Product : Entity
    {        
        public string Name { get; set; }
                
        public string Description { get; set; }
                
        public string Image { get; set; }
                
        public decimal Price { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public bool Active { get; set; }
        
        public Guid SupplierId { get; set; }
        
        public Supplier Supplier { get; set; }
    }
}
