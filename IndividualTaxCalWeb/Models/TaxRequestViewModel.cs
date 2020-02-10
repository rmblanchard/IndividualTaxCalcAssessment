using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualTaxCalWeb.Models
{
    public class TaxCalcRequestViewModel
    {
        [Required]
        [Display(Name = "Postal Code")]
        public string SelectedPostalCode { get; set; }

        public IEnumerable<SelectListItem> PostalCodes { get; set; }

        [Required]
        [Display(Name = "Annual Income")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal AnnualIncome { get; set; }

        [Display(Name = "Tax Amount Due")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TaxAmount { get; set; }
    }
}