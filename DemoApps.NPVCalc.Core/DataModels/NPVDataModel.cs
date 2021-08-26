using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoApps.NPVCalc.Core.DataModels
{
    public class NPVDataModel
    {
        public NPVDataModel()
        {
            NPVPerDiscountRate = new List<string>();
        }

        [Required]
        [Display(Name = "Initial Investment")]
        [DataType(DataType.Currency)]
        public decimal InitialInvestment { get; set; }

        [Required]
        [Display(Name = "Lower Bound Discount")]
        [DataType(DataType.Currency)]
        public decimal LowerBoundDiscountRate { get; set; }

        [Required]
        [Display(Name = "Upper Bound Discount")]
        [DataType(DataType.Currency)]
        public decimal UpperBoundDiscountRate { get; set; }

        [Required]
        [Display(Name = "Discount Rate Increment")]
        [DataType(DataType.Currency)]
        public decimal DiscountRateIncrement { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal[] Cashflows { get; set; }

        public List<string> NPVPerDiscountRate { get; set; }
    }
}
