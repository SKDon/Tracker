using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;

namespace Tracker.Areas.Admin.Models.State
{
	public sealed class StateEditModel
	{
		[HiddenInput]
		public long Id { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "StateName")]
		public string Name { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Position")]
		public int Position { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "LocalizedName")]
		public string LocalizedName { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Language")]
		public string Language { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "EnableEmailSend")]
		public bool EnableEmailSend { get; set; }
	}
}