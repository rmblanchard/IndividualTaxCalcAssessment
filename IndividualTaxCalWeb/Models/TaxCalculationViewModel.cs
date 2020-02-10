using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualTaxCalWeb.Models
{
    public class TaxCalculationViewModel
    {
        public string PostalCode { get; set; }

        public double AnnualIncome { get; set; }

        public double TaxAmount { get; set; }
    }
}