using System;

namespace Tracker.MvcHelpers.BundleHelpres
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class PathAttribute : Attribute
	{
		public string[] Paths { get; set; }

		public PathAttribute(params string[] paths)
		{
			Paths = paths;
		}
	}
}