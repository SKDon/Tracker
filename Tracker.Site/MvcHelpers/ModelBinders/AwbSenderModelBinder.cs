using System.Web;
using System.Web.Mvc;
using Tracker.MvcHelpers.Extensions;
using Tracker.ViewModels.AirWaybill;

namespace Tracker.MvcHelpers.ModelBinders
{
	internal sealed class AwbSenderModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var model = (AwbSenderModel)base.BindModel(controllerContext, bindingContext);

			ReadFiles(controllerContext.HttpContext.Request, model);

			return model;
		}

		private static void ReadFiles(HttpRequestBase request, AwbSenderModel model)
		{
			if (model.PackingFile == null && model.PackingFileName == null)
				request.ReadFile("PackingFile", (name, bytes) =>
				{
					model.PackingFileName = name;
					model.PackingFile = bytes;
				});

			if (model.AWBFile == null && model.AWBFileName == null)
				request.ReadFile("AWBFile", (name, bytes) =>
				{
					model.AWBFileName = name;
					model.AWBFile = bytes;
				});
		}
	}
}