﻿using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Jobs.Helpers.Abstract;
using Tracker.Utilities;

namespace Tracker.Jobs.Client.ClientAdd
{
	internal sealed class CommonLocalizedDataHelper : ILocalizedDataHelper
	{
		private readonly IClientRepository _clients;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;

		public CommonLocalizedDataHelper(ISerializer serializer, IClientRepository clients, ISenderRepository senders)
		{
			_serializer = serializer;
			_clients = clients;
			_senders = senders;
		}

		public IDictionary<string, string> Get(string language, EventDataForEntity eventData)
		{
			var client = _clients.Get(eventData.EntityId);

			var password = _serializer.Deserialize<string>(eventData.Data);

			return new Dictionary<string, string>
			{
				{ "ClientNic", client.Nic },
				{ "LegalEntity", client.LegalEntity },
				{ "Password", password },
				{ "Login", client.Login },
				{ "SenderName", GetSenderName(client) }
			};
		}

		private string GetSenderName(ClientEditData client)
		{
			return client.DefaultSenderId.HasValue
				? _senders.Get(client.DefaultSenderId.Value).Name
				: null;
		}
	}
}