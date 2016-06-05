using System.Web.Mvc;
using Tracker.Core.Contracts.AirWaybill;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Exceptions;
using Tracker.MvcHelpers.Filters;
using Tracker.Services.Abstract;
using Tracker.Services.AirWaybill;
using Tracker.ViewModels.AirWaybill;
using Resources;

namespace Tracker.Controllers.Awb
{
	[Access(RoleType.Sender)]
	public partial class SenderAwbController : Controller
	{
		private readonly IAwbManager _awbManager;
		private readonly IAwbPresenter _awbPresenter;
		private readonly IAwbUpdateManager _awbUpdateManager;
		private readonly IIdentityService _identity;

		public SenderAwbController(
			IIdentityService identity,
			IAwbUpdateManager awbUpdateManager,
			IAwbManager awbManager,
			IAwbPresenter awbPresenter)
		{
			_identity = identity;
			_awbUpdateManager = awbUpdateManager;
			_awbManager = awbManager;
			_awbPresenter = awbPresenter;
		}

		[HttpGet]
		public virtual ActionResult Create(long? id)
		{
			BindBag(null);

			return View();
		}

		[HttpPost]
		public virtual ActionResult Create(long? id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var airWaybillData = AwbMapper.GetData(model, _identity.Id);

					_awbManager.Create(id, airWaybillData, _identity.Id);

					return RedirectToAction(MVC.AirWaybill.Index());
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(null);

			return View(model);
		}

		[HttpGet]
		public virtual ViewResult Edit(long id)
		{
			var model = _awbPresenter.GetSenderAwbModel(id);

			BindBag(id);

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Edit(long id, AwbSenderModel model)
		{
			try
			{
				if(ModelState.IsValid)
				{
					_awbUpdateManager.Update(id, model);

					return RedirectToAction(MVC.SenderAwb.Edit(id));
				}
			}
			catch(DublicateException)
			{
				ModelState.AddModelError("Bill", Validation.AirWaybillAlreadyExists);
			}

			BindBag(id);

			return View(model);
		}

		private void BindBag(long? awbId)
		{
			ViewBag.AwbId = awbId;
		}
	}
}