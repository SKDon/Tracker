using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.Areas.Admin.Serivices.Abstract
{
	public interface IBillPdf
	{
		FileHolder Get(long applicationId);
	}
}