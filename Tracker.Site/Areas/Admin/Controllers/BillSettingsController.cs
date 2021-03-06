﻿using System;
using System.Web.Mvc;
using Tracker.Areas.Admin.Models;
using Tracker.Core.Contracts.Calculation;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Exceptions;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.MvcHelpers.Filters;
using Tracker.Utilities;

namespace Tracker.Areas.Admin.Controllers
{
	[Access(RoleType.Admin)]
	public partial class BillSettingsController : Controller
	{
		private readonly ISerializer _serializer;
		private readonly ISettingRepository _settings;

		public BillSettingsController(ISerializer serializer, ISettingRepository settings)
		{
			_serializer = serializer;
			_settings = settings;
		}

		[HttpGet]
		public virtual ActionResult Index()
		{
			var setting = _settings.Get(SettingType.Bill);

			var billSettings = _serializer.Deserialize<BillSettings>(setting.Data);

			var model = new BillSettingsModel
			{
				BankDetails = new BankDetails
				{
					Bank = billSettings.Bank,
					BIC = billSettings.BIC,
					CorrespondentAccount = billSettings.CorrespondentAccount,
					CurrentAccount = billSettings.CurrentAccount,
					Payee = billSettings.Payee,
					TaxRegistrationReasonCode = billSettings.TaxRegistrationReasonCode,
					TIN = billSettings.TIN
				},
				Version = setting.RowVersion,
				Accountant = billSettings.Accountant,
				Head = billSettings.Head,
				HeaderText = billSettings.HeaderText,
				Shipper = billSettings.Shipper,
				VAT = (byte)(billSettings.VAT * 100),
				EuroToRuble = billSettings.EuroToRuble,
				SourceUrl = billSettings.SourceUrl,
				AutoUpdatePeriodHours =
					billSettings.AutoUpdatePeriod.HasValue ? billSettings.AutoUpdatePeriod.Value.Hours : (int?)null
			};

			return View(model);
		}

		[HttpPost]
		public virtual ActionResult Index(BillSettingsModel model)
		{
			if(model.EuroToRuble <= 0)
			{
				ModelState.AddModelError("EuroToRuble", @"EUR/RUB must have positive value");
			}

			if(model.VAT <= 0)
			{
				ModelState.AddModelError("VAT", @"VAT must have positive value");
			}

			if(!ModelState.IsValid)
			{
				return View(model);
			}

			var data = _serializer.Serialize(new BillSettings
			{
				Accountant = model.Accountant,
				Bank = model.BankDetails.Bank,
				BIC = model.BankDetails.BIC,
				CorrespondentAccount = model.BankDetails.CorrespondentAccount,
				CurrentAccount = model.BankDetails.CurrentAccount,
				Head = model.Head,
				HeaderText = model.HeaderText,
				Payee = model.BankDetails.Payee,
				Shipper = model.Shipper,
				TaxRegistrationReasonCode = model.BankDetails.TaxRegistrationReasonCode,
				TIN = model.BankDetails.TIN,
				VAT = (decimal)model.VAT / 100,
				EuroToRuble = model.EuroToRuble,
				SourceUrl = model.SourceUrl,
				AutoUpdatePeriod = model.AutoUpdatePeriodHours.HasValue
					? TimeSpan.FromHours(model.AutoUpdatePeriodHours.Value)
					: (TimeSpan?)null
			});

			try
			{
				_settings.AddOrReplace(new Setting
				{
					Data = data,
					RowVersion = model.Version,
					Type = SettingType.Bill
				});
			}
			catch(UpdateConflictException)
			{
				ModelState.AddModelError("Version", @"Outdated data, refresh the page and try again");

				return View(model);
			}

			return RedirectToAction(MVC.Admin.BillSettings.Index());
		}
	}
}