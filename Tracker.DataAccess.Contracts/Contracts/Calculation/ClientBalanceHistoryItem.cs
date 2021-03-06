﻿using System;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Contracts.Calculation
{
	public sealed class ClientBalanceHistoryItem
	{
		public DateTimeOffset Timestamp { get; set; }
		public EventType EventType { get; set; }
		public decimal Balance { get; set; }
		public decimal Money { get; set; }
		public string Comment { get; set; }
		public bool IsCalculation { get; set; }
	}
}