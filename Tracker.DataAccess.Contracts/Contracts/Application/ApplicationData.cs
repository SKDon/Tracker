﻿using System;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Utilities;

namespace Tracker.DataAccess.Contracts.Contracts.Application
{
	public sealed class ApplicationData : ApplicationEditData
	{
		public long Id { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
		public long StateId { get; set; }
		public DateTimeOffset StateChangeTimestamp { get; set; }
		public int DisplayNumber { get; set; }

		public string ClientLegalEntity { get; set; }
		public string ClientNic { get; set; }
		public long ClientUserId { get; set; }
		public string ClientEmails { get; set; }

		public string AirWaybill { get; set; }
		public string AirWaybillGTD { get; set; }
		public DateTimeOffset? AirWaybillDateOfDeparture { get; set; }
		public DateTimeOffset? AirWaybillDateOfArrival { get; set; }

		public long TransitCityId { get; set; }
		public string TransitAddress { get; set; }
		public string TransitRecipientName { get; set; }
		public string TransitPhone { get; set; }
		public string TransitWarehouseWorkingTime { get; set; }
		public MethodOfTransit TransitMethodOfTransit { get; set; }
		public DeliveryType TransitDeliveryType { get; set; }

		public long CarrierId { get; set; }
		public string CarrierName { get; set; }
		public string CarrierPhone { get; set; }
		public string CarrierAddress { get; set; }
		public string CarrierContact { get; set; }
		public string CarrierEmail { get; set; }

		public string ForwarderName { get; set; }
		public decimal? SenderScotchCost { get; set; }
		public string SenderName { get; set; }
		public string SenderContact { get; set; }
		public string SenderPhone { get; set; }
		public string SenderAddress { get; set; }
		public string SenderEmail { get; set; }

		public decimal? GetAdjustedFactureCost()
		{
			return FactureCostEdited ?? FactureCost;
		}

		public decimal? GetAdjustedFactureCostEx()
		{
			return FactureCostExEdited ?? FactureCostEx;
		}

		public decimal? GetAdjustedPickupCost()
		{
			return PickupCostEdited ?? PickupCost;
		}

		public decimal? GetAdjustedScotchCost()
		{
			return ScotchCostEdited ?? SenderScotchCost;
		}

		public decimal? GetAdjustedTransitCost()
		{
			return TransitCostEdited ?? TransitCost;
		}

		public string GetApplicationDisplay()
		{
			return String.Format("{0:000}{1}", DisplayNumber % DisplayNumberShard, Count.HasValue && Count > 0 ? "/" + Count.Value : "");
		}

		public const int DisplayNumberShard = 1000;

		public int GetDaysInWork()
		{
			return (DateTimeProvider.Now - CreationTimestamp.ToUniversalTime()).Days;
		}
	}
}