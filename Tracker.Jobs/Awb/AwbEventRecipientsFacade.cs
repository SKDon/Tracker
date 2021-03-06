﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Helpers;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Jobs.Helpers.Abstract;

namespace Tracker.Jobs.Awb
{
	public sealed class AwbEventRecipientsFacade : IRecipientsFacade
	{
		private readonly IAdminRepository _admins;
		private readonly IManagerRepository _managers;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IEventEmailRecipient _recipients;

		public AwbEventRecipientsFacade(
			IAdminRepository admins,
			IManagerRepository managers,
			IBrokerRepository brokers,
			IAwbRepository awbs,
			IEventEmailRecipient recipients)
		{
			_admins = admins;
			_managers = managers;
			_brokers = brokers;
			_awbs = awbs;
			_recipients = recipients;
		}

		public RecipientData[] GetRecipients(EventType type, EventDataForEntity data)
		{
			var roles = _recipients.GetRecipientRoles(type);

			return roles.Length == 0
				? null
				: GetRecipients(roles, data).ToArray();
		}

		private IEnumerable<RecipientData> GetRecipients(IEnumerable<RoleType> roles, EventDataForEntity data)
		{
			foreach(var role in roles)
			{
				switch(role)
				{
					case RoleType.Admin:
						{
							var recipients = _admins.GetAll()
								.Select(user => new RecipientData(user.Email, user.Language, RoleType.Admin));
							foreach(var recipient in recipients)
							{
								yield return recipient;
							}
						}
						break;

					case RoleType.Manager:
						{
							var recipients = _managers.GetAll()
								.Select(user => new RecipientData(user.Email, user.Language, RoleType.Manager));
							foreach(var recipient in recipients)
							{
								yield return recipient;
							}
						}
						break;

					case RoleType.Broker:
						var brokerId = _awbs.Get(data.EntityId).Single().BrokerId;
						if(brokerId.HasValue)
						{
							var broker = _brokers.Get(brokerId.Value);

							yield return new RecipientData(broker.Email, broker.Language, role);
						}

						break;

					case RoleType.Sender:
						foreach(var email in _awbs.GetSenderEmails(data.EntityId))
						{
							yield return new RecipientData(email.Email, email.Language, RoleType.Client);
						}

						break;

					case RoleType.Client:
						foreach(var emailData in _awbs.GetClientEmails(data.EntityId))
						{
							foreach(var email in EmailsHelper.SplitAndTrimEmails(emailData.Email))
							{
								yield return new RecipientData(email, emailData.Language, RoleType.Client);
							}
						}

						break;

					case RoleType.Forwarder:
						foreach(var email in _awbs.GetForwarderEmails(data.EntityId))
						{
							yield return new RecipientData(email.Email, email.Language, RoleType.Client);
						}

						break;

					case RoleType.Carrier:
						foreach(var email in _awbs.GetCarrierEmails(data.EntityId))
						{
							yield return new RecipientData(email.Email, email.Language, RoleType.Client);
						}

						break;

					default:
						throw new InvalidOperationException("Unknown role " + role);
				}
			}
		}
	}
}