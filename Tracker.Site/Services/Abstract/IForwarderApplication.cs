using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
	public interface IForwarderApplication
	{
		void UpdateDeliveryData(ApplicationListItem[] applicationItems, string language);
	}
}