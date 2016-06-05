using System.Linq;
using Tracker.Core.Contracts.Client;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Exceptions;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Services.Abstract;
using Tracker.ViewModels.User;

namespace Tracker.Services.Users.Client
{
	internal sealed class ClientPresenter : IClientPresenter
	{
		private readonly IClientBalanceRepository _balances;
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;
		private readonly IClientPermissions _permissions;

		public ClientPresenter(
			IClientRepository clients,
			IIdentityService identity,
			IClientBalanceRepository balances,
			IClientPermissions permissions)
		{
			_clients = clients;
			_identity = identity;
			_balances = balances;
			_permissions = permissions;
		}

		public ClientData GetCurrentClientData(long? clientId = null)
		{
			ClientData data;

			if(clientId.HasValue)
			{
				data = _clients.Get(clientId.Value);
			}
			else if(_identity.IsAuthenticated)
			{
				data = _clients.GetByUserId(_identity.Id);
			}
			else
			{
				return null;
			}

			if(!_permissions.HaveAccessToClient(data))
			{
				throw new AccessForbiddenException();
			}

			return data;
		}

		public ListCollection<ClientListItem> GetList(int take, int skip)
		{
			var all = _clients.GetAll().OrderBy(x => x.Nic).ToArray();

			var data = all.Skip(skip).Take(take)
				.Select(x => new ClientListItem
				{
					Nic = x.Nic,
					LegalEntity = x.LegalEntity,
					Balance = _balances.GetBalance(x.ClientId),
					ClientId = x.ClientId
				})
				.ToArray();

			return new ListCollection<ClientListItem>
			{
				Data = data,
				Total = all.Length
			};
		}
	}
}