using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.MvcHelpers.Filters;
using Tracker.Services.Abstract;
using Tracker.ViewModels;

namespace Tracker.Controllers
{
	public partial class CountryController : Controller
	{
		private readonly ICountryService _countries;
		private readonly IIdentityService _identity;

		public CountryController(
			ICountryService countries,
			IIdentityService identity)
		{
			_countries = countries;
			_identity = identity;
		}

		[Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Index()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Manager), OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public virtual JsonResult List(int take, int skip)
		{
			var list = _countries.List(take, skip, _identity.Language);

			return Json(list);
		}

		[HttpGet, Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Edit(long id)
		{
			var model = _countries.Get(id);

			return View(model);
		}

		[HttpGet, Access(RoleType.Admin, RoleType.Manager)]
		public virtual ViewResult Create()
		{
			return View();
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Edit(long id, CountryEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_countries.Edit(id, model);

			return RedirectToAction(MVC.Country.Edit(id));
		}

		[HttpPost, Access(RoleType.Admin, RoleType.Manager)]
		public virtual ActionResult Create(CountryEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var id = _countries.Add(model);

			return RedirectToAction(MVC.Country.Edit(id));
		}
	}
}
