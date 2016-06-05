using System.IO;

namespace Tracker.Core.Contracts.Calculation
{
	public interface IExcelClientCalculation
	{
		MemoryStream Get(long clientId, string language);
	}
}
