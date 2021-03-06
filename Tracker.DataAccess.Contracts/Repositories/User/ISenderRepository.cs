﻿using System.Collections.Generic;
using Tracker.DataAccess.Contracts.Contracts.User;

namespace Tracker.DataAccess.Contracts.Repositories.User
{
	public interface ISenderRepository
	{
		long? GetByUserId(long userId);
		SenderData Get(long senderId);
		Dictionary<long, decimal> GetTariffs(params long[] ids);
		long Add(SenderData data, string password);
		void Update(long senderId, SenderData data);
		long GetUserId(long senderId);
		UserEntityData[] GetAll();

		long[] GetByCountry(long countryId);
		long[] GetCountries(long senderId);
		void SetCountries(long senderId, long[] countries);
	}
}