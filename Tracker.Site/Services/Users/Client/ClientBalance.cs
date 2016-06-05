using System;
using Tracker.Core.Contracts.Client;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;

namespace Tracker.Services.Users.Client
{
	internal sealed class ClientBalance : IClientBalance
	{
		private readonly IClientBalanceRepository _balance;

		public ClientBalance(IClientBalanceRepository balance)
		{
			_balance = balance;
		}

		public void Increase(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation)
		{
			var balance = _balance.GetBalance(clientId);

			balance += money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, money, EventType.BalanceIncreased, timestamp, comment, isCalculation);
		}

		public void Decrease(long clientId, decimal money, string comment, DateTimeOffset timestamp, bool isCalculation)
		{
			var balance = _balance.GetBalance(clientId);

			balance -= money;

			_balance.SetBalance(clientId, balance);

			_balance.AddToHistory(clientId, balance, money, EventType.BalanceDecreased, timestamp, comment, isCalculation);
		}
	}
}