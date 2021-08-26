using System;
using System.Linq;
using System.Threading.Tasks;
using DemoApps.NPVCalc.Core.DataModels;
using DemoApps.NPVCalc.Services;
using DemoApps.NPVCalc.Services.Interfaces;
using Xunit;

namespace DemoApps.NPVCalc.UnitTests
{
    public class FinancialFunctionsTest
    {
        [Fact]
        public async Task ComputeNPV()
        {
            IFinancialService func = new FinancialService();

            decimal expectedResult = (decimal)620.92;
            decimal initialInvestment = 100000;
            decimal discountRate = 10;
            decimal[] cashflows = {
                10000,
                10000,
                10000,
                20000,
                100000
            };

            var npvAmount = await func.ComputeNPV(initialInvestment,
                discountRate,
                cashflows);

            Assert.Equal(expectedResult, Math.Round(npvAmount, 2));
        }

        [Fact]
        public async Task ComputeNPVPerDiscountRate()
        {
            IFinancialService func = new FinancialService();
            var input = new NPVDataModel
            { 
                InitialInvestment = 100000,
                DiscountRateIncrement = (decimal)0.5,
                LowerBoundDiscountRate = 5,
                UpperBoundDiscountRate = 15,
                Cashflows = new decimal[] {
                    10000,
                    10000,
                    10000,
                    20000,
                    100000
                }
            };

            for (var discountRate = input.LowerBoundDiscountRate;
                    discountRate <= input.UpperBoundDiscountRate;
                    discountRate += input.DiscountRateIncrement) {

                decimal npv = await func.ComputeNPV(input.InitialInvestment,
                    discountRate,
                    input.Cashflows.ToArray());

                input.NPVPerDiscountRate.Add(Math.Round(npv, 2));
            }
        }
    }
}
