using System;
using Tracker.Utilities;

namespace Tracker.TestHelpers
{
	public sealed class DateTimeProviderStub : DateTimeProvider.IDateTimeProvider
	{
		public DateTimeProviderStub(DateTimeOffset now)
		{
			Now = now;
		}

		public DateTimeOffset Now { get; private set; }
	}
}