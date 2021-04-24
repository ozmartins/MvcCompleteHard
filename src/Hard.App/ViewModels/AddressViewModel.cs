using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        [StringLength(200)]
        public string Number { get; set; }

        [StringLength(50)]
        public string Complement { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Zip code")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string Neighborhood { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("City")]
        public string CityName { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }
        
        [HiddenInput]
        public SupplierViewModel Supplier { get; set; }
    }
}
