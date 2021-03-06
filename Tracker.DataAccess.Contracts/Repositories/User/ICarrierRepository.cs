﻿using Tracker.DataAccess.Contracts.Contracts.User;

namespace Tracker.DataAccess.Contracts.Repositories.User
{
	public interface ICarrierRepository
	{
		void Update(long id, string name, string email, string phone, string contact, string address, string login);
		long Add(string name, string email, string phone, string contact, string address, string login, string password, string language);
		CarrierData[] GetAll();
		CarrierData Get(long id);

		long[] GetCities(long carrierId);
		void SetCities(long carrierId, long[] cities);
		long[] GetByCity(long cityId);
		long? GetByUserId(long userId);
	}
}