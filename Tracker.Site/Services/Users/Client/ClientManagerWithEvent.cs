using Tracker.Core.Contracts.Event;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Services.Abstract;
using Tracker.ViewModels;
using Tracker.ViewModels.User;

namespace Tracker.Services.Users.Client
{
	internal sealed class ClientManagerWithEvent : IClientManager
	{
		private readonly IEventFacade _events;
		private readonly IClientManager _manager;

		public ClientManagerWithEvent(IClientManager manager, IEventFacade events)
		{
			_manager = manager;
			_events = events;
		}

		public void Update(long clientId, ClientModel model, TransitEditModel transit)
		{
			_manager.Update(clientId, model, transit);
		}

		public long Add(ClientModel client, TransitEditModel transit)
		{
			var id = _manager.Add(client, transit);

			_events.Add(id, EventType.ClientAdded, EventState.Emailing, client.Authentication.NewPassword);

			return id;
		}
	}
}