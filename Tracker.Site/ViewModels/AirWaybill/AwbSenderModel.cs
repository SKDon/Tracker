using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;

namespace Tracker.ViewModels.AirWaybill
{
	public sealed class AwbSenderModel
	{
		[Required]
		[DisplayNameLocalized(typeof(Entities), "AirWaybill")]
		public string Bill { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "ArrivalAirport")]
		public string ArrivalAirport { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DepartureAirport")]
		public string DepartureAirport { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfDeparture")]
		public string DateOfDepartureLocalString { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "DateOfArrival")]
		public string DateOfArrivalLocalString { get; set; }

		[DisplayNameLocalized(typeof(Entities), "FlightCost")]
		public decimal? FlightCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "TotalCostOfSenderForWeight")]
		public decimal? TotalCostOfSenderForWeight { get; set; }
	}
}