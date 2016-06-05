using System.Linq;
using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.MvcHelpers.Filters;
using Tracker.Utilities.Localization;
using Tracker.ViewModels.Calculation.Admin;

namespace Tracker.Controllers.Calculation
{
	[Access(RoleType.Admin, RoleType.Manager)]
	public partial class RegistryOfPaymentsController : Controller
	{
		private readonly IClientBalanceRepository _clientBalance;

		public RegistryOfPaymentsController(IClientBalanceRepository clientBalance)
		{
			_clientBalance = clientBalance;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var data = _clientBalance.GetRegistryOfPayments();

			return Json(new ListCollection<RegistryOfPaymentsItem>
			{
				Data = data.Select(x => new RegistryOfPaymentsItem
				{
					ClientNic = x.ClientNic,
					Comment = x.Comment,
					EventType = x.EventType.ToLocalString(),
					Money = x.Money,
					Timestamp = x.Timestamp.Date.ToShortDateString(),
					Balance = x.Balance
				}).ToArray(),
				Groups = null,
				Total = data.Length
			});
		}
	}
}