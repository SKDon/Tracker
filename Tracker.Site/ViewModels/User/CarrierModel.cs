using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;
using Resources;

namespace Tracker.ViewModels.User
{
	public sealed class CarrierModel
	{
		public AuthenticationModel Authentication { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Pages), "Name")]
		public string Name { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Phone")]
		public string Phone { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Contacts")]
		public string Contact { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Email")]
		[DataType(DataType.EmailAddress)]
		[MaxLength(320)]
		public string Email { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Cities")]
		public long[] Cities { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Address")]
		public string Address { get; set; }
	}
}