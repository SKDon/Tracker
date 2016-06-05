using System;
using Tracker.Core.Contracts.Client;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;

namespace Tracker.Services.Users.Client
{
	internal sealed class ClientPermissions : IClientPermissions
	{
		private readonly IClientRepository _clients;
		private readonly IIdentityService _identity;

		public ClientPermissions(
			IIdentityService identity,
			IClientRepository clients)
		{
			_identity = identity;
			_clients = clients;
		}

		public bool HaveAccessToClient(ClientData data)
		{
			if(_identity.IsInRole(RoleType.Admin) || _identity.IsInRole(RoleType.Sender) || _identity.IsInRole(RoleType.Manager))
			{
				return true;
			}

			if(data == null)
			{
				throw new ArgumentNullException("data");
			}

			var client = _clients.GetByUserId(_identity.Id);

			return client != null && client.ClientId == data.ClientId;
		}
	}
}