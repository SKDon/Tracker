﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Tracker.Core.Resources;
using Tracker.Utilities.Localization;
using EventType = Tracker.DataAccess.Contracts.Enums.EventType;

namespace Tracker.ViewModels.EmailTemplate
{
	public sealed class EventTemplateModel
	{
		[HiddenInput]
		public EventType EventType { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Subject")]
		public string Subject { get; set; }

		[DisplayNameLocalized(typeof(Entities), "Body")]
		[DataType(DataType.MultilineText)]
		public string Body { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "Language")]
		public string Language { get; set; }

		[Required, DisplayNameLocalized(typeof(Entities), "EnableEmailSend")]
		public bool EnableEmailSend { get; set; }

		public EmailTemplateSettingsModel Settings { get; set; }
	}
}