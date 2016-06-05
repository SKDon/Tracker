using System.Web.Optimization;

namespace Tracker.MvcHelpers.BundleHelpres
{
	internal sealed class StyleRelativePathTransformBundle : Bundle
	{
		public StyleRelativePathTransformBundle(string virtualPath)
			: base(virtualPath, new StyleRelativePathTransform(), new CssMinify()) { }
	}
}