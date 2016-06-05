using System.Linq;
using System.Web.Mvc;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.ViewModels.Application;

namespace Tracker.Controllers.Application
{
	public partial class ApplicationPrintController : Controller
	{
		private readonly IApplicationRepository _applications;
		private readonly ICityRepository _cities;

		public ApplicationPrintController(IApplicationRepository applications, ICityRepository cities)
		{
			_applications = applications;
			_cities = cities;
		}

		public virtual ActionResult Index(long id)
		{
			var data = _applications.Get(id);

			var city = _cities.All(TwoLetterISOLanguageName.English).Single(x => x.Id == data.TransitCityId);

			return View(
				new ApplicationPrintModel
				{
					City = city.Name,
					ClientNic = data.ClientNic,
					Text = data.GetApplicationDisplay()
				});
		}
	}
}