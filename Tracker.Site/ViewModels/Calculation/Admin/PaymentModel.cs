using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;

namespace Tracker.ViewModels.Calculation.Admin
{
	public sealed class PaymentModel
	{
		[Required]
		[DataType(DataType.Currency)]
		[DisplayNameLocalized(typeof(Entities), "Sum")]
		public decimal? Money { get; set; }

		[DataType(DataType.MultilineText)]
		[DisplayNameLocalized(typeof(Entities), "Comment")]
		public string Comment { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[DisplayNameLocalized(typeof(Entities), "Date")]
		public string Timestamp { get; set; }
	}
}