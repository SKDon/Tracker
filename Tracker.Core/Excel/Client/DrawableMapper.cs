using System.Linq;
using Tracker.Core.Contracts.Excel;
using Tracker.DataAccess.Contracts.Contracts.Calculation;
using Tracker.DataAccess.Contracts.Contracts.User;
using OfficeOpenXml;

namespace Tracker.Core.Excel.Client
{
	internal static class DrawableMapper
	{
		public static IDrawable[] Get(CalculationData[] calculations, ExcelWorksheet excel, ClientData client, int columnCount)
		{
			return calculations.Select(x => (IDrawable)new CalculationDataDrawable(
				x, client.Nic, columnCount, excel)).ToArray();
		}

		public static IDrawable[] Get(ClientBalanceHistoryItem[] history, ExcelWorksheet excel, ClientData client,
			int columnCount)
		{
			return history.Select(x => (IDrawable)new ClientBalanceHistoryItemDrawable(
				x, columnCount, excel)).ToArray();
		}
	}
}