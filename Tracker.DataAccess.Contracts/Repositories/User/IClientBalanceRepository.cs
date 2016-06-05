using System;
using Tracker.DataAccess.Contracts.Contracts.Calculation;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories.User
{
	public interface IClientBalanceRepository
	{
		void AddToHistory(
			long clientId, decimal balance, decimal money, EventType type, DateTimeOffset timestamp,
			string comment, bool isCalculation);
		decimal GetBalance(long clientId);
		ClientBalanceHistoryItem[] GetHistory(long clientId);
		void SetBalance(long clientId, decimal balance);
		decimal SumBalance();
		RegistryOfPaymentsData[] GetRegistryOfPayments();
	}
}