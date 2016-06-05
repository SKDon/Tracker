using Tracker.ViewModels.Calculation.Admin;

namespace Tracker.Services.Abstract
{
	public interface IAdminCalculationPresenter
	{
		CalculationListCollection List(int take, long skip);
		CalculationListCollection Row(long awbId);
	}
}