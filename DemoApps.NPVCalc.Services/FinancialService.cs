using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApps.NPVCalc.Services.Interfaces;

namespace DemoApps.NPVCalc.Services
{
    public class FinancialService: IFinancialService
    {
        public async Task<decimal> ComputeNPV(decimal initialInvestment,
            decimal discountRate,
            decimal[] cashflows)
        {
            // compute discount factors.
            var discountFactors = new List<decimal>();

            for (int i = 0; i < cashflows.Length; i++) {
                decimal dfIncrement = (discountRate / 100) + 1;
                decimal dfValue = (decimal)(1 / (Math.Pow((double)dfIncrement, i+1)));
                discountFactors.Add(cashflows[i] * dfValue);
            }

            // compute npv.
            decimal npv = 0;
            decimal total = 0;

            for (int i = 0; i < cashflows.Length; i++) {
                npv += discountFactors[i];
                total += cashflows[i];
            }

            // compute final npv.
            return await Task.FromResult(npv - initialInvestment);
        }
    }
}
