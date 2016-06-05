using Tracker.Core.Resources;
using Tracker.Utilities.Localization;

namespace Tracker.ViewModels.AirWaybill
{
	public sealed class AwbBrokerModel
	{
		[DisplayNameLocalized(typeof(Entities), "GTD")]
		public string GTD { get; set; }

		[DisplayNameLocalized(typeof(Entities), "CustomCost")]
		public decimal? CustomCost { get; set; }

		[DisplayNameLocalized(typeof(Entities), "BrokerCost")]
		public decimal? BrokerCost { get; set; }
	}
}