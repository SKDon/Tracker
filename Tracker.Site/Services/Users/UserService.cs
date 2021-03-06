﻿using System;
using System.Linq;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Services.Abstract;
using Tracker.ViewModels;
using Tracker.ViewModels.User;

namespace Tracker.Services.Users
{
	internal sealed class UserService : IUserService
	{
		private readonly IAdminRepository _admins;
		private readonly IManagerRepository _managers;
		private readonly ICarrierRepository _carriers;
		private readonly ISenderRepository _senders;
		private readonly IBrokerRepository _brokers;
		private readonly IForwarderRepository _forwarders;
		private readonly IUserRepository _users;

		public UserService(IUserRepository users,
			IAdminRepository admins,
			IManagerRepository managers,
			ICarrierRepository carriers,
			ISenderRepository senders,
			IForwarderRepository forwarders,
			IBrokerRepository brokers)
		{
			_users = users;
			_admins = admins;
			_managers = managers;
			_carriers = carriers;
			_senders = senders;
			_forwarders = forwarders;
			_brokers = brokers;
		}

		public UserListItem[] List(RoleType role)
		{
			switch(role)
			{
				case RoleType.Manager:
					return _managers.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.EntityId,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Admin:
					return _admins.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.EntityId,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Broker:
					return _brokers.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.Id,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Forwarder:
					return _forwarders.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.Id,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Carrier:
					return _carriers.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.Id,
							UserId = x.UserId
						}).ToArray();

				case RoleType.Sender:
					return _senders.GetAll().Select(
						x => new UserListItem
						{
							Name = x.Name,
							EntityId = x.EntityId,
							UserId = x.UserId
						}).ToArray();

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}

		public UserModel Get(RoleType role, long id)
		{
			var data = GetByRole(role, id);

			return new UserModel
			{
				Id = data.EntityId,
				Name = data.Name,
				RoleType = role,
				Email = data.Email,
				Authentication = new AuthenticationModel
				{
					Login = data.Login
				}
			};
		}

		public void Update(UserModel model)
		{
			long userId;
			switch(model.RoleType)
			{
				case RoleType.Admin:
					userId = _admins.Update(model.Id, model.Name, model.Authentication.Login, model.Email);
					break;

				case RoleType.Manager:
					userId = _managers.Update(model.Id, model.Name, model.Authentication.Login, model.Email);
					break;

				case RoleType.Broker:
					userId = _brokers.Update(model.Id, model.Name, model.Authentication.Login, model.Email);
					break;

				default:
					throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			}

			if(!string.IsNullOrEmpty(model.Authentication.NewPassword))
			{
				_users.SetPassword(userId, model.Authentication.NewPassword);
			}
		}

		public void Add(UserModel model)
		{
			long userId;
			switch(model.RoleType)
			{
				case RoleType.Admin:
					userId = _admins.Add(model.Name, model.Authentication.Login, model.Email, TwoLetterISOLanguageName.English);
					break;

				case RoleType.Manager:
					userId = _managers.Add(model.Name, model.Authentication.Login, model.Email, TwoLetterISOLanguageName.English);
					break;

				case RoleType.Broker:
					userId = _brokers.Add(model.Name, model.Authentication.Login, model.Email, TwoLetterISOLanguageName.English);
					break;

				default:
					throw new ArgumentOutOfRangeException("model", @"Unknown role " + model.RoleType);
			}

			_users.SetPassword(userId, model.Authentication.NewPassword);
		}

		private UserEntityData GetByRole(RoleType role, long id)
		{
			switch(role)
			{
				case RoleType.Admin:
					return _admins.GetAll().First(x => x.EntityId == id);

				case RoleType.Manager:
					return _managers.GetAll().First(x => x.EntityId == id);

				case RoleType.Broker:
					var broker = _brokers.Get(id);
					return new UserEntityData
					{
						Email = broker.Email,
						Language = broker.Language,
						Login = broker.Login,
						EntityId = broker.Id,
						Name = broker.Name,
						UserId = broker.UserId
					};

				default:
					throw new ArgumentOutOfRangeException("role");
			}
		}
	}
}