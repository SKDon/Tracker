using System.Linq;
using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.ViewModels;
using Antlr.Runtime.Misc;

namespace Tracker.Controllers
{
	public partial class TransitController : Controller
	{
		private readonly IIdentityService _identity;
		private readonly ICityRepository _cities;
		private readonly ITransitRepository _transits;

		public TransitController(
			IIdentityService identity,
			ICityRepository cities,
			ITransitRepository transits)
		{
			_identity = identity;
			_cities = cities;
			_transits = transits;
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByApplication(long? applicationId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			ViewBag.Cities = _cities.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);


			return !applicationId.HasValue
				? PartialView()
				: GetEditPartialView(() => _transits.GetByApplication(applicationId.Value));
		}

		private PartialViewResult GetEditPartialView(Func<TransitData> getData)
		{
			var data = getData();

			var transit = TransitMapper.Map(data);

			ViewBag.TransitId = data.Id;

			return PartialView(transit);
		}

		[ChildActionOnly]
		public virtual PartialViewResult EditByClient(long? clientId)
		{
			ViewData.TemplateInfo.HtmlFieldPrefix = "Transit";

			ViewBag.Cities = _cities.All(_identity.Language).ToDictionary(x => x.Id, x => x.Name);

			return !clientId.HasValue
				? PartialView()
				: GetEditPartialView(() => _transits.GetByClient(clientId.Value));
		}
	}
}