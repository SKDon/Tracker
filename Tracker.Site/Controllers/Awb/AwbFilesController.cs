﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tracker.Core.Contracts.Event;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.MvcHelpers.Extensions;

namespace Tracker.Controllers.Awb
{
	public partial class AwbFilesController : Controller
	{
		private static readonly IReadOnlyDictionary<AwbFileType, EventType> TypesMapping
			= new Dictionary<AwbFileType, EventType>
			{
				{ AwbFileType.AWB, EventType.AWBFileUploaded },
				{ AwbFileType.GTD, EventType.GTDFileUploaded },
				{ AwbFileType.Other, EventType.OtherAwbFileUploaded },
				{ AwbFileType.GTDAdditional, EventType.GTDAdditionalFileUploaded },
				{ AwbFileType.Invoice, EventType.AwbInvoiceFileUploaded },
				{ AwbFileType.Packing, EventType.AwbPackingFileUploaded },
				{ AwbFileType.Draw, EventType.DrawFileUploaded },
			};

		private readonly IEventFacade _facade;
		private readonly IAwbFileRepository _files;

		public AwbFilesController(IAwbFileRepository files, IEventFacade facade)
		{
			_files = files;
			_facade = facade;
		}

		[ChildActionOnly]
		public virtual PartialViewResult Admin(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[ChildActionOnly]
		public virtual PartialViewResult Broker(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual HttpStatusCodeResult Delete(long id)
		{
			_files.Delete(id);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		[HttpGet]
		public virtual FileResult Download(long id)
		{
			var file = _files.Get(id);

			return file.GetFileResult();
		}

		[HttpGet]
		public virtual JsonResult Files(long id, AwbFileType type)
		{
			var names = _files.GetNames(id, type);

			ViewBag.AwbId = id;

			return Json(names.Select(x => new
			{
				id = x.Id,
				name = x.Name
			}),
				JsonRequestBehavior.AllowGet);
		}

		[ChildActionOnly]
		public virtual PartialViewResult Sender(long id)
		{
			ViewBag.AwbId = id;

			return PartialView();
		}

		[HttpPost]
		public virtual JsonResult Upload(long id, AwbFileType type, HttpPostedFileBase file)
		{
			var bytes = file.GetBytes();

			var fileId = _files.Add(id, type, file.FileName, bytes);

			AddFileUploadEvent(id, TypesMapping[type], file.FileName, bytes);

			return Json(new { id = fileId });
		}

		private void AddFileUploadEvent(
			long awbId, EventType type,
			string fileName, byte[] fileData)
		{
			if(fileData == null || fileData.Length == 0) return;

			_facade.Add(awbId,
				type,
				EventState.Emailing,
				new FileHolder
				{
					Data = fileData,
					Name = fileName
				});
		}
	}
}