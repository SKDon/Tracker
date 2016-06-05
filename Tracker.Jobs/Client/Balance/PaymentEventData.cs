using System;

namespace Tracker.Jobs.Client.Balance
{
	public sealed class PaymentEventData
	{
		public decimal Money { get; set; }
		public string Comment { get; set; }
		public DateTimeOffset Timestamp { get; set; }
	}
}