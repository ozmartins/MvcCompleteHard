using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hard.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        [DisplayName("Document")]
        public string DocumentId { get; set; }

        [DisplayName("Document type")]
        public int DocumentType { get; set; }

        public AddressViewModel Address { get; set; }

        public bool Active { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
