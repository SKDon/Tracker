using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;
using Resources;

namespace Tracker.ViewModels
{
	public sealed class CountryEditModel
	{
		[Required, DisplayNameLocalized(typeof(Pages), "Russian")]
		public string RussianName { get; set; }

		[Required, DisplayNameLocalized(typeof(Pages), "English")]
		public string EnglishName { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }
	}
}