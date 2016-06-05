using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
	public interface IApplicationPresenter
	{
		ApplicationAdminModel Get(long id);
		ApplicationStateModel[] GetStateAvailability(long id);
	}
}