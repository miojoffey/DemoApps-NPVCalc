using System.Threading.Tasks;

namespace DemoApps.NPVCalc.Services.Interfaces
{
    public interface IFinancialService
    {
        Task<decimal> ComputeNPV(decimal initialInvestment,
            decimal discountRate,
            decimal[] cashflows);
    }
}
