using System.Web.Mvc;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.State;
using Ninject;

namespace Tracker.MvcHelpers
{
    public abstract class BaseWebViewPage : WebViewPage
	{
		[Inject]
		public IIdentityService IdentityService { get; set; }

		[Inject]
		public IStateConfig StateConfig { get; set; }
	}

	public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
	{
		[Inject]
		public IIdentityService IdentityService { get; set; }

		[Inject]
		public IStateConfig StateConfig { get; set; }
	}
}