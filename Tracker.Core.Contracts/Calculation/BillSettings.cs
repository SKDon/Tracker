﻿using System;

namespace Tracker.Core.Contracts.Calculation
{
	public sealed class BillSettings
	{		
		public string Bank { get; set; }
		public string BIC { get; set; }
		public string CorrespondentAccount { get; set; }
		public string TIN { get; set; }
		public string TaxRegistrationReasonCode { get; set; }
		public string CurrentAccount { get; set; }
		public string Payee { get; set; }
		public string Shipper { get; set; }
		public string Head { get; set; }
		public string Accountant { get; set; }
		public string HeaderText { get; set; }
		public decimal VAT { get; set; }
		public decimal EuroToRuble { get; set; } // EUR/RUB
		public TimeSpan? AutoUpdatePeriod { get; set; }
		public string SourceUrl { get; set; }
	}
}