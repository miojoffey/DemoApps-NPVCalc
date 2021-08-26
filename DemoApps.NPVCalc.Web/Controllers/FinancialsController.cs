using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApps.NPVCalc.Core.DataModels;
using DemoApps.NPVCalc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoApps.NPVCalc.Web.Controllers
{
    public class FinancialsController : Controller
    {
        readonly IFinancialService _financialService;

        public FinancialsController(IFinancialService financialService)
        {
            _financialService = financialService;
        }

        public ActionResult NPV()
        {
            return View(new NPVDataModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NPV(NPVDataModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            for (var discountRate = model.LowerBoundDiscountRate;
                    discountRate <= model.UpperBoundDiscountRate;
                    discountRate += model.DiscountRateIncrement) {

                decimal npv = await _financialService.ComputeNPV(model.InitialInvestment,
                    discountRate,
                    model.Cashflows.ToArray());

                model.NPVPerDiscountRate.Add($"Discount rate of {discountRate}% has NPV of ${Math.Round(npv, 2)}");
            }

            return View(model);
        }   
    }
}
