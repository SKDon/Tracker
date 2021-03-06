﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Helpers;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.MvcHelpers.Filters;
using Tracker.Services;
using Tracker.Utilities.Localization;
using Tracker.ViewModels.EmailTemplate;
using Tracker.ViewModels.Helpers;

namespace Tracker.Controllers
{
	public partial class EmailTemplateController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly IEventEmailRecipient _recipients;
		private readonly ITemplateRepository _templates;

		public EmailTemplateController(
			ITemplateRepository templates,
			IEventEmailRecipient recipients,
			IIdentityService identity)
		{
			_templates = templates;
			_recipients = recipients;
			_identity = identity;
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Edit(EventType id, string lang)
		{
			BindLanguageList();

			var model = GetModel(id, lang ?? _identity.Language);

			return View(model);
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Edit(EventTemplateModel model)
		{
			if(!ModelState.IsValid)
			{
				BindLanguageList();

				return View(model);
			}

			var roles = model.Settings.GetSettings();

			_recipients.Set(model.EventType, roles);

			_templates.SetForEvent(
				model.EventType,
				model.Language,
				model.EnableEmailSend,
				new EmailTemplateLocalizationData
				{
					Body = model.Body,
					IsBodyHtml = false,
					Subject = model.Subject
				});

			return RedirectToAction(MVC.EmailTemplate.Edit(model.EventType, model.Language));
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Help()
		{
			return View();
		}

		[HttpGet]
		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Index()
		{
			return View();
		}

		[HttpPost]
		[Access(RoleType.Admin, RoleType.Manager)]
		[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List()
		{
			var types = Enum.GetValues(typeof(EventType))
				.Cast<EventType>()
				.Select(
					x => new
					{
						Id = (int)x,
						Order = EventHelper.ApplicationEventTypes.Contains(x)
							? 1
							: EventHelper.AwbEventTypes.Contains(x)
								? 2
								: 3,
						Name = x.ToLocalString()
					})
				.OrderBy(x => x.Order)
				.ThenBy(x => x.Name)
				.Select(
					x => new
					{
						x.Id,
						x.Name
					})
				.ToArray();

			return Json(types);
		}

		private void BindLanguageList()
		{
			ViewBag.Languages = new Dictionary<string, string>
			{
				{ TwoLetterISOLanguageName.English, LanguageName.English },
				{ TwoLetterISOLanguageName.Russian, LanguageName.Russian },
				{ TwoLetterISOLanguageName.Italian, LanguageName.Italian },
				{ TwoLetterISOLanguageName.Polish, LanguageName.Polish }
			};
		}

		private EventTemplateModel GetModel(EventType eventType, string language)
		{
			var commonData = _templates.GetByEventType(eventType);

			var localization = commonData != null ? _templates.GetLocalization(commonData.EmailTemplateId, language) : null;

			var recipients = _recipients.GetRecipientRoles(eventType);

			return new EventTemplateModel
			{
				Body = localization != null ? localization.Body : null,
				Subject = localization != null ? localization.Subject : null,
				EnableEmailSend = commonData != null && commonData.EnableEmailSend,
				Language = language,
				EventType = eventType,
				Settings = EmailTemplateSettingsModelHelper.GetModel(recipients)
			};
		}
	}
}