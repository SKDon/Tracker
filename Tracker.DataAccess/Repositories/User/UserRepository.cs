﻿using System;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Utilities;

namespace Tracker.DataAccess.Repositories.User
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly IPasswordConverter _converter;
		private readonly ISqlProcedureExecutor _executor;

		public UserRepository(IPasswordConverter converter, ISqlProcedureExecutor executor)
		{
			_converter = converter;
			_executor = executor;
		}

		public void SetLanguage(long userId, string language)
		{
			_executor.Execute("[dbo].[User_SetLanguage]", new { userId, language });
		}

		public void SetLogin(long userId, string login)
		{
			_executor.Execute("[dbo].[User_SetLogin]", new { userId, login });
		}

		public UserData Get(long userId)
		{
			return _executor.Query<UserData>("[dbo].[User_Get]", new { userId });
		}

		public long? GetUserIdByEmail(string email)
		{
			return _executor.Query<long?>("[dbo].[User_GetUserIdByEmail]", new { email });
		}

		public void SetPassword(long userId, string password)
		{
			if(password == null)
			{
				throw new ArgumentNullException("password");
			}

			var salt = _converter.GenerateSalt();
			var hash = _converter.GetPasswordHash(password, salt);

			_executor.Execute("[dbo].[User_SetPassword]", new { userId, salt, hash });
		}

		public PasswordData GetPasswordData(string login)
		{
			return _executor.Query<PasswordData>("[dbo].[User_GetPasswordData]", new { login });
		}

		public long Add(string login, string password, string language)
		{
			var salt = _converter.GenerateSalt();
			var hash = _converter.GetPasswordHash(password, salt);

			return _executor.Query<long>("[dbo].[User_Add]",
				new
				{
					Login = login,
					PasswordSalt = salt,
					PasswordHash = hash,
					@TwoLetterISOLanguageName = language
				});
		}
	}
}