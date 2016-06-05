using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.ViewModels.User;

namespace Tracker.Services.Abstract
{
	public interface IClientPresenter
	{
		ClientData GetCurrentClientData(long? clientId = null);
		ListCollection<ClientListItem> GetList(int take, int skip);
	}
}
