using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;

namespace Tracker.Areas.Admin.Models.State
{
	public sealed class StateCreateModel
	{
		[Required, DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }
	}
}