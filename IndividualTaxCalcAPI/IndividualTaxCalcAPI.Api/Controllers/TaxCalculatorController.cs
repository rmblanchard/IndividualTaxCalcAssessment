using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using IndividualTaxCalcAPI.Domain;
using IndividualTaxCalcAPI.Entity;
using IndividualTaxCalcAPI.Domain.Service;

namespace IndividualTaxCalcAPI.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly TaxCalculationService<TaxCalculationViewModel, TaxCalculation> _taxCalculationService;

        public TaxCalculatorController(IConfiguration configuration,
                                        TaxCalculationService<TaxCalculationViewModel, TaxCalculation> taxCalculationService)
        {
            Configuration = configuration;
            _taxCalculationService = taxCalculationService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaxCalcRequestViewModel taxRequest)
        {
            if (taxRequest == null)
            {
                return BadRequest();
            }

            //check if a valid post code has been selected
            switch (taxRequest.PostalCode)
            {
                case "7441":
                case "1000":
                    taxRequest.TaxAmount = CalculateProgressiveTax(taxRequest.AnnualIncome);
                    break;

                case "A100":
                    taxRequest.TaxAmount = CalculateFlatValueTax(taxRequest.AnnualIncome);
                    break;

                case "7000":
                    taxRequest.TaxAmount = CalculateFlatRateTax(taxRequest.AnnualIncome);
                    break;

                default:
                    return BadRequest();
            }

            TaxCalculationViewModel taxCalcViewModel = new TaxCalculationViewModel();
            taxCalcViewModel.PostalCode = taxRequest.PostalCode;
            taxCalcViewModel.AnnualIncome = taxRequest.AnnualIncome;
            taxCalcViewModel.TaxAmount = taxRequest.TaxAmount;

            var id = _taxCalculationService.Add(taxCalcViewModel);
            //TODO: Write to TaxCalc table

            return Ok(taxRequest);
        }

        private double CalculateProgressiveTax(double annualIncome)
        {
            //TODO: Progressive Tax Calculation

            double taxAmt = 0;

            if (annualIncome >= 372951)
                taxAmt = ((annualIncome - 372950) * 0.35) + 108216.00;
            else
            {
                if (annualIncome >= 171551)
                {
                    taxAmt = ((annualIncome - 171550) * 0.33) + 41754.00;
                }
                else
                {
                    if (annualIncome >= 82251)
                    {
                        taxAmt = ((annualIncome - 82250) * 0.28) + 16750.00;
                    }
                    else
                    {
                        if (annualIncome >= 33951)
                        {
                            taxAmt = ((annualIncome - 33950) * 0.25) + 4675.00;
                        }
                        else
                        {
                            if (annualIncome >= 8351)
                            {
                                taxAmt = ((annualIncome - 8351) * 0.15) + 835.00;
                            }
                            else
                            {
                                taxAmt = annualIncome * 0.10;
                            }
                        }
                    }
                }
            }

            return taxAmt;
        }

        private double CalculateFlatRateTax(double annualIncome)
        {
            return annualIncome * 0.175;
        }

        private double CalculateFlatValueTax(double annualIncome)
        {
            if (annualIncome < 20000)
                return annualIncome * 0.05;
            else
                return 10000;
        }
    }

    public class TaxCalcRequestViewModel
    {
        public string PostalCode { get; set; }

        public double AnnualIncome { get; set; }

        public double TaxAmount { get; set; }
    }
}