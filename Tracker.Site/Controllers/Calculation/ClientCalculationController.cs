﻿using System.Web.Mvc;
using Tracker.Core.Contracts.Calculation;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.MvcHelpers.Filters;
using Tracker.Services.Abstract;

namespace Tracker.Controllers.Calculation
{
	public partial class ClientCalculationController : Controller
	{
		private readonly IClientBalanceRepository _balance;
		private readonly IClientRepository _clients;
		private readonly IExcelClientCalculation _excel;
		private readonly IIdentityService _identity;
		private readonly IClientCalculationPresenter _presenter;

		public ClientCalculationController(
			IClientCalculationPresenter presenter,
			IClientBalanceRepository balance,
			IExcelClientCalculation excel,
			IClientRepository clients,
			IIdentityService identity)
		{
			_presenter = presenter;
			_balance = balance;
			_excel = excel;
			_clients = clients;
			_identity = identity;
		}

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ActionResult Index()
		{
			var client = _clients.GetByUserId(_identity.Id);

			var balance = _balance.GetBalance(client.ClientId);

			ViewBag.Balance = balance;
			ViewBag.ClientId = client.ClientId;

			return View();
		}

		[HttpGet]
		[Access(RoleType.Client)]
		public virtual ViewResult History(long clientId)
		{
			var balance = _balance.GetBalance(clientId);

			ViewBag.Balance = balance;

			return View(clientId);
		}

		[HttpPost]
		[Access(RoleType.Client)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, long skip)
		{
			var client = _clients.GetByUserId(_identity.Id);

			var data = _presenter.List(client.ClientId, take, skip);

			return Json(data);
		}

		[Access(RoleType.Client)]
		public virtual FileResult Excel()
		{
			var client = _clients.GetByUserId(_identity.Id);

			var stream = _excel.Get(client.ClientId, _identity.Language);

			return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calculation.xlsx");
		}
	}
}