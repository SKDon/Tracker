using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Utilities.Localization;

namespace Tracker.ViewModels
{
	public sealed class TransitEditModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "MethodOfTransit")]
		public MethodOfTransit MethodOfTransit { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "DeliveryType")]
		public DeliveryType DeliveryType { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "City")]
		public long CityId { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Address")]
		public string Address { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "RecipientName")]
		public string RecipientName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Phone")]
		public string Phone { get; set; }

		[DisplayNameLocalized(typeof(Entities), "WarehouseWorkingTime")]
		public string WarehouseWorkingTime { get; set; }
	}
}