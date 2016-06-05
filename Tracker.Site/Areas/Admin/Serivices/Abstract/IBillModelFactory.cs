using Tracker.Areas.Admin.Models;
using Tracker.DataAccess.Contracts.Contracts.Application;

namespace Tracker.Areas.Admin.Serivices.Abstract
{
	public interface IBillModelFactory
	{
		BillModel GetBillModel(BillData bill);
		BillModel GetBillModelByApplication(long applicationId);
	}
}