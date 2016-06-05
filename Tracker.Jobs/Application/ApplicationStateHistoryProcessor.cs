using System;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Jobs.Core;

namespace Tracker.Jobs.Application
{
	internal sealed class ApplicationStateHistoryProcessor : IEventProcessor
	{
		public void ProcessEvent(EventType type, EventData data)
		{
			if (type != EventType.ApplicationSetState)
			{
				throw new InvalidOperationException(type + " is not supported");
			}
		}
	}
}