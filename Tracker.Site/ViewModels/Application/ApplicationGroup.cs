﻿using Tracker.Core.Helpers;

namespace Tracker.ViewModels.Application
{
	public sealed class ApplicationGroup
	{
		// ReSharper disable InconsistentNaming
		public Aggregates aggregates { get; set; }
		public string field { get; set; }
		public string value { get; set; }
		public bool hasSubgroups { get; set; }
		public object[] items { get; set; }
		// ReSharper restore InconsistentNaming

		public sealed class Aggregates
		{
			public Aggregates(int count, float weight, decimal value, float volume, float documentWeight)
			{
				Count = new SumHolder<int>(count);
				Weight = new SumHolder<float>(weight);
				DocumentWeight = new SumHolder<float>(documentWeight);
				Value = new SumHolder<decimal>(value);	
				Volume = new SumHolder<float>(volume);
			}

			public SumHolder<int> Count { get; private set; }
			public SumHolder<float> Weight { get; private set; }
			public SumHolder<float> DocumentWeight { get; private set; }
			public SumHolder<decimal> Value { get; private set; }
			public SumHolder<float> Volume { get; private set; }
		}
	}
}