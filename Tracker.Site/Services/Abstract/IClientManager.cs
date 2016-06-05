using Tracker.ViewModels;
using Tracker.ViewModels.User;

namespace Tracker.Services.Abstract
{
	public interface IClientManager
	{
		void Update(long clientId, ClientModel model, TransitEditModel transit);
		long Add(ClientModel client, TransitEditModel transit);
	}
}