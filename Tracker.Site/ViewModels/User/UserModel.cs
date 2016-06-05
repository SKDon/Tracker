using System.ComponentModel.DataAnnotations;
using Tracker.Core.Resources;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Utilities.Localization;
using Resources;

namespace Tracker.ViewModels.User
{
	public sealed class UserModel
	{
		public AuthenticationModel Authentication { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Pages), "Name")]
		public string Name { get; set; }

		[Required]
		public long Id { get; set; }

		public RoleType RoleType { get; set; }

		[Required]
		[DisplayNameLocalized(typeof(Entities), "Email")]
		[DataType(DataType.EmailAddress)]
		[MaxLength(320)]
		public string Email { get; set; }
	}
}