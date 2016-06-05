using System;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Email;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.Utilities;

namespace Tracker.Core.Email
{
	public sealed class SilentMailSender : IMailSender
	{
		private readonly IMailSender _sender;
		private readonly ILog _log;

		public SilentMailSender(IMailSender sender, ILog log)
		{
			_sender = sender;
			_log = log;
		}

		public void Send(params EmailMessage[] messages)
		{
			try
			{
				_sender.Send(messages);
			}
			catch (Exception ex)
			{
				if (ex.IsCritical()) throw;

				_log.Error("Failed to send a message", ex);
			}
		}
	}
}