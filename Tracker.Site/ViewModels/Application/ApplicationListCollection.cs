﻿namespace Tracker.ViewModels.Application
{
    public sealed class ApplicationListCollection
	{
		public ApplicationGroup[] Groups { get; set; }
		public ApplicationListItem[] Data { get; set; }
		public long Total { get; set; }
	}
}
