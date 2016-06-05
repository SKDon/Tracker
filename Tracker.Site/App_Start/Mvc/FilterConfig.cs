using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.MvcHelpers.Filters;
using Ninject;

namespace Tracker.Mvc
{
	internal static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
		{
			filters.Add(new CustomHandleErrorAttribute(kernel.Get<ILog>()));

			filters.Add(new CultureFilterAttribute(CompositionRootHelper.GetLanguage(kernel)));

			filters.Add(new AccessAuthorizationFilter(() => kernel.Get<IIdentityService>()));
		}
	}
}