using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
	public interface ISenderApplicationManager
	{
		void Add(ApplicationSenderModel model, ClientData clientId, long creatorSenderId);
		ApplicationSenderModel Get(long id);
		void Update(long id, ApplicationSenderModel model);
	}
}
