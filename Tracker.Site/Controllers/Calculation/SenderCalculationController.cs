using System.IO;
using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.MvcHelpers.Filters;
using Tracker.Services.Abstract;
using Tracker.Services.Excel;

namespace Tracker.Controllers.Calculation
{
	public partial class SenderCalculationController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly ISenderCalculationPresenter _presenter;
		private readonly ISenderRepository _senders;

		public SenderCalculationController(ISenderCalculationPresenter presenter, ISenderRepository senders,
			IIdentityService identity)
		{
			_presenter = presenter;
			_senders = senders;
			_identity = identity;
		}

		[Access(RoleType.Sender)]
		public virtual FileResult Excel()
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			if(!senderId.HasValue)
			{
				throw new InvalidDataException();
			}

			var data = _presenter.List(senderId.Value, int.MaxValue, 0);

			var excel = new ExcelSenderCalculation();

			var stream = excel.Get(data, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}

		[HttpGet]
		[Access(RoleType.Sender)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Sender)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var senderId = _senders.GetByUserId(_identity.Id);

			if(!senderId.HasValue)
			{
				throw new InvalidDataException();
			}

			var data = _presenter.List(senderId.Value, take, skip);

			return Json(data);
		}
	}
}