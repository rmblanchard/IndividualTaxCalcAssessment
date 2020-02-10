using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IndividualTaxCalWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;

namespace IndividualTaxCalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TaxCalcRequestViewModel model = new TaxCalcRequestViewModel();

            model = SetUpPostalCodes(model);

            return View(model);
        }

        private TaxCalcRequestViewModel SetUpPostalCodes(TaxCalcRequestViewModel model)
        {
            List<SelectListItem> postCodeList = new List<SelectListItem>();
            postCodeList.Add(new SelectListItem() { Value = "7441", Text = "7441" });
            postCodeList.Add(new SelectListItem() { Value = "1000", Text = "1000" });
            postCodeList.Add(new SelectListItem() { Value = "A100", Text = "A100" });
            postCodeList.Add(new SelectListItem() { Value = "7000", Text = "7000" });

            model.PostalCodes = postCodeList;
            return model;
        }

        [HttpPost]
        public IActionResult Index(TaxCalcRequestViewModel taxModel)
        {
            TaxCalculationViewModel taxCalcModel = new TaxCalculationViewModel();
            taxCalcModel.PostalCode = taxModel.SelectedPostalCode;
            taxCalcModel.AnnualIncome = Double.Parse(taxModel.AnnualIncome.ToString());
            taxCalcModel.TaxAmount = 0;

            var PostResult = PostAsync(taxCalcModel).GetAwaiter().GetResult();

            taxModel = SetUpPostalCodes(taxModel);
            return View(taxModel);
        }

        public async Task<string> PostAsync(TaxCalculationViewModel taxModel)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(new Uri("http://localhost:44341/api/TaxCalculation"), new StringContent(JsonConvert.SerializeObject(taxModel)));

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => content);
        }

        public string GetTaxAmount(TaxCalcRequestViewModel tModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44341/api/");

                var postTask = client.PostAsync("TaxCalculation", new StringContent(JsonConvert.SerializeObject(tModel)));

                var result = postTask.Result.Content.ReadAsStringAsync();
                result.Wait();

                string resultContent = result.ToString();

                return resultContent;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}