using System;
using Tracker.Core.Contracts.Common;

namespace Tracker.TestHelpers
{
	internal sealed class ConsoleLogger : ILog
	{
		public void Error(string message, Exception exception)
		{
			Console.WriteLine(message + exception);
		}

		public void Warning(string message)
		{
			Console.WriteLine(message);
		}

		public void Info(string message)
		{
			Console.WriteLine(message);
		}
	}
}