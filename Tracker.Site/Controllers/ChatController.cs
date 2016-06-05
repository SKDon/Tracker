using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Repositories.User;

namespace Tracker.Controllers
{
	public partial class ChatController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IUserRepository _users;

		public ChatController(
			IIdentityService identity,
			IUserRepository users)
		{
			_identity = identity;
			_users = users;
		}

		public virtual ActionResult Index()
		{
			var userData = _users.Get(_identity.Id);

			return View(userData);
		}
	}
}