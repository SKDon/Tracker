using Tracker.Services.Excel.Rows;

namespace Tracker.Services.Abstract
{
	public interface IApplicationExcelRowSource
	{
		AdminApplicationExcelRow[] GetAdminApplicationExcelRow(string language);
		ForwarderApplicationExcelRow[] GetForwarderApplicationExcelRow(long forwarderId, string language);
		SenderApplicationExcelRow[] GetSenderApplicationExcelRow(long senderId, string language);
		CarrierApplicationExcelRow[] GetCarrierApplicationExcelRow(long carrierId, string language);
	}
}