using Tracker.Core.Calculation;
using Tracker.DataAccess.Contracts.Contracts.Calculation;

namespace Tracker.Core.Helpers
{
	public static class CalculationDataHelper
	{
		public static decimal GetMoney(CalculationData calculation)
		{
			return GetMoney(calculation, calculation.InsuranceRate);
		}

		public static decimal GetMoney(CalculationData calculation, float insuranceRate)
		{
			if(calculation.Profit.HasValue)
			{
				return calculation.Profit.Value;
			}

			return CalculationHelper.GetTotalTariffCost(
				calculation.TotalTariffCost,
				calculation.TariffPerKg,
				calculation.Weight)
			       + calculation.ScotchCost
			       + calculation.Value * (decimal)insuranceRate
			       + calculation.FactureCost
			       + calculation.FactureCostEx
			       + calculation.TransitCost
			       + calculation.PickupCost;
		}
	}
}