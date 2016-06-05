using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Utilities.Localization;

namespace Tracker.ViewModels
{
	public sealed class CurrencyModel
	{
		public CurrencyModel()
		{
			CurrencyId = CurrencyType.Euro;
		}

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public CurrencyType CurrencyId { get; set; }
	}
}