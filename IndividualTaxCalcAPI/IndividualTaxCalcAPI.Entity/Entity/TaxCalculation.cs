using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IndividualTaxCalcAPI.Entity
{
    public class TaxCalculation : BaseEntity
    {
        //public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(4)]
        public string PostalCode { get; set; }

        public double AnnualIncome { get; set; }

        public double TaxAmount { get; set; }
    }
}