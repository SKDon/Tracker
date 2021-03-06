﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.DataAccess.DbContext;

namespace Tracker.DataAccess.Repositories.User
{
	public sealed class AdminRepository : IAdminRepository
	{
		private readonly TrackerDataContext _context;
		private readonly Expression<Func<Admin, UserEntityData>> _selector;

		public AdminRepository(IDbConnection connection)
		{
			_context = new TrackerDataContext(connection);

			_selector = x => new UserEntityData
			{
				EntityId = x.Id,
				UserId = x.UserId,
				Name = x.Name,
				Login = x.User.Login,
				Email = x.Email,
				Language = x.User.TwoLetterISOLanguageName
			};
		}

		public long Update(long adminId, string name, string login, string email)
		{
			var entity = _context.Admins.First(x => x.Id == adminId);
			entity.Name = name;
			entity.User.Login = login;
			entity.Email = email;

			_context.SaveChanges();

			return entity.UserId;
		}

		public long Add(string name, string login, string email, string language)
		{
			var user = new DbContext.User
			{
				Login = login,
				TwoLetterISOLanguageName = language,
				PasswordHash = new byte[0],
				PasswordSalt = new byte[0]
			};

			_context.Admins.InsertOnSubmit(new Admin
			{
				Name = name,
				User = user,
				Email = email
			});

			_context.SaveChanges();

			return user.Id;
		}

		public UserEntityData[] GetAll()
		{			
			return _context.Admins.Select(_selector).ToArray();
		}
	}
}