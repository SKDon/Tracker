using Tracker.ViewModels.User;

namespace Tracker.Services.Abstract
{
	public interface ISenderService
	{
		SenderModel Get(long id);
		long Add(SenderModel model);
		void Update(long id, SenderModel model);
	}
}