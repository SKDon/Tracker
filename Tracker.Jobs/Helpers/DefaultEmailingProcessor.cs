using Tracker.Core.Contracts.Email;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Jobs.Core;
using Tracker.Jobs.Helpers.Abstract;

namespace Tracker.Jobs.Helpers
{
	internal sealed class DefaultEmailingProcessor : IEventProcessor
	{
		private readonly IMessageBuilder _messageBuilder;
		private readonly IMailSender _sender;

		public DefaultEmailingProcessor(
			IMailSender sender,
			IMessageBuilder messageBuilder)
		{
			_sender = sender;
			_messageBuilder = messageBuilder;
		}

		public void ProcessEvent(EventType type, EventData data)
		{
			var messages = _messageBuilder.Get(type, data);

			if (messages != null)
			{
				_sender.Send(messages);
			}
		}
	}
}