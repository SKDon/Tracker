﻿using System;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Contracts.Application
{
	public class ApplicationEditData
	{
		public string Invoice { get; set; }
		public string Characteristic { get; set; }
		public string AddressLoad { get; set; }
		public string WarehouseWorkingTime { get; set; }
		public float? Weight { get; set; }
		public int? Count { get; set; }
		public float Volume { get; set; }
		public string TermsOfDelivery { get; set; }
		public decimal Value { get; set; }
		public CurrencyType CurrencyId { get; set; }
		public long CountryId { get; set; }
		public DateTimeOffset? DateInStock { get; set; }
		public DateTimeOffset? DateOfCargoReceipt { get; set; }
		public string FactoryName { get; set; }
		public string FactoryPhone { get; set; }
		public string FactoryEmail { get; set; }
		public string FactoryContact { get; set; }
		public string MarkName { get; set; }
		public string TransitReference { get; set; }
		public MethodOfDelivery MethodOfDelivery { get; set; }
		public long ClientId { get; set; }
		public long TransitId { get; set; }
		public long? AirWaybillId { get; set; }
		public long? SenderId { get; set; }
		public long ForwarderId { get; set; }
		public ClassType? Class { get; set; }
		public bool IsPickup { get; set; }

		public decimal? FactureCost { get; set; }
		public decimal? FactureCostEx { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal? FactureCostEdited { get; set; }
		public decimal? FactureCostExEdited { get; set; }
		public decimal? TransitCostEdited { get; set; }
		public decimal? ScotchCostEdited { get; set; }
		public decimal? PickupCostEdited { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? SenderRate { get; set; }
		public decimal? CalculationTotalTariffCost { get; set; }
		public decimal? CalculationProfit { get; set; }
		public float InsuranceRate { get; set; }

		public string MRN { get; set; }
		public int? CountInInvoce { get; set; }
		public float? DocumentWeight { get; set; }

	    public string Comments { get; set; }
	}
}