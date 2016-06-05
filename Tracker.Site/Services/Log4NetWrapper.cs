using System;
using Tracker.Core.Contracts.Common;

namespace Tracker.Services
{
	internal sealed class Log4NetWrapper : ILog
	{
		private readonly log4net.ILog _log;

		public Log4NetWrapper(log4net.ILog log)
		{
			_log = log;

			Info("The logger is created");
		}

		public void Error(string message, Exception exception)
		{
			_log.Error(message, exception);
		}

		public void Warning(string message)
		{
			_log.Warn(message);
		}

		public void Info(string message)
		{
			_log.Info(message);
		}
	}
}