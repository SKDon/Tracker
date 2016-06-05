using System.Linq;
using System.Net;
using System.Web.Mvc;
using Tracker.Core.Contracts.AirWaybill;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Exceptions;
using Tracker.Core.Contracts.State;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.MvcHelpers.Filters;
using Tracker.Services.Abstract;
using Tracker.Utilities;
using Tracker.ViewModels;
using Microsoft.Ajax.Utilities;

namespace Tracker.Controllers.Awb
{
	public partial class AirWaybillController : Controller
	{
		private readonly IApplicationAwbManager _applicationAwbManager;
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbStateManager _awbStateManager;
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IStateConfig _config;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;

		public AirWaybillController(
			IAwbPresenter awbPresenter,
			IApplicationAwbManager applicationAwbManager,
			IAwbManager awbManager,
			IStateConfig config,
			IAwbRepository awbs,
			IAwbStateManager awbStateManager,
			ISenderRepository senders,
			IBrokerRepository brokers,
			IIdentityService identity)
		{
			_awbPresenter = awbPresenter;
			_brokers = brokers;
			_applicationAwbManager = applicationAwbManager;
			_awbManager = awbManager;
			_config = config;
			_awbs = awbs;
			_awbStateManager = awbStateManager;
			_senders = senders;
			_brokers = brokers;
			_identity = identity;
		}

		#region List

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker, RoleType.Sender)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker, RoleType.Sender)]
		public virtual JsonResult List(int take, int skip)
		{
			var brokerId = _brokers.GetByUserId(_identity.Id).With(x => (int?)x.Id);

			var senderUserId = _senders.GetByUserId(_identity.Id).With(x => _identity.Id, (long?)null);

			var list = _awbPresenter.List(take, skip, brokerId, senderUserId, _identity.Language);

			return Json(list);
		}

		#endregion

		#region Actions

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Broker)]
		public virtual HttpStatusCodeResult CargoIsCustomsCleared(long id)
		{
			var data = _awbs.Get(id).First();
			if(data.GTD.IsNullOrWhiteSpace())
			{
				throw new InvalidLogicException("GTD must be definded to set the CargoIsCustomsCleared state");
			}

			_awbStateManager.SetState(id, _config.CargoIsCustomsClearedStateId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Sender)]
		public virtual HttpStatusCodeResult SetActive(long id, bool isActive)
		{
			_awbs.SetActive(id, isActive);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[ChildActionOnly]
		public virtual PartialViewResult CargoIsCustomsClearedButton(long id)
		{
			var data = _awbs.Get(id).First();

			var model = new CargoIsCustomsClearedButtonModel
			{
				Id = id,
				CanSetCargoIsCustomsCleared =
					data.StateId == _config.CargoAtCustomsStateId && !data.GTD.IsNullOrWhiteSpace()
			};

			return PartialView("CargoIsCustomsClearedButton", model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Sender)]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_awbManager.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager, RoleType.Sender)]
		public virtual ActionResult SetAirWaybill(long applicationId, long? airWaybillId)
		{
			_applicationAwbManager.SetAwb(applicationId, airWaybillId);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		#endregion
	}
}