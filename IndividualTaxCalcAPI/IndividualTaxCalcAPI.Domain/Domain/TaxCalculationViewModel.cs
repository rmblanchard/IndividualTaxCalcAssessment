using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;

namespace IndividualTaxCalcAPI.Domain
{
    public class TaxCalculationViewModel : BaseDomain
    {
        public string PostalCode { get; set; }

        public double AnnualIncome { get; set; }

        public double TaxAmount { get; set; }
    }
}