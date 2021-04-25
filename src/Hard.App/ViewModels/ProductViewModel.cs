using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [DisplayName("Image")]
        public IFormFile UploadImage { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }

        public bool Active { get; set; }

        [Required]
        [DisplayName("Supplier")]
        public Guid SupplierId { get; set; }

        [HiddenInput]
        [NotMapped]
        public SupplierViewModel Supplier { get; set; }

        [HiddenInput]
        [NotMapped]
        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
    }
}
